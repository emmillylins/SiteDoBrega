
document.addEventListener('DOMContentLoaded', () => {
  "use strict";
  
  /* Mobile nav toggle */
  const mobileNavShow = document.querySelector('.mobile-nav-show');
  const mobileNavHide = document.querySelector('.mobile-nav-hide');

  document.querySelectorAll('.mobile-nav-toggle').forEach(el => {
    el.addEventListener('click', function(event) {
      event.preventDefault();
      mobileNavToogle();
    })
  });

  function mobileNavToogle() {
    document.querySelector('body').classList.toggle('mobile-nav-active');
    mobileNavShow.classList.toggle('d-none');
    mobileNavHide.classList.toggle('d-none');
  }

  /* Hide mobile nav on same-page/hash links */
  document.querySelectorAll('#navbar a').forEach(navbarlink => {
    if (!navbarlink.hash) return;

    let section = document.querySelector(navbarlink.hash);
    if (!section) return;

    navbarlink.addEventListener('click', () => {
      if (document.querySelector('.mobile-nav-active')) {
        mobileNavToogle();
      }
    });

  });

  /* Toggle mobile nav dropdowns */
  const navDropdowns = document.querySelectorAll('.navbar .dropdown > a');

  navDropdowns.forEach(el => {
    el.addEventListener('click', function(event) {
      if (document.querySelector('.mobile-nav-active')) {
        event.preventDefault();
        this.classList.toggle('active');
        this.nextElementSibling.classList.toggle('dropdown-active');

        let dropDownIndicator = this.querySelector('.dropdown-indicator');
        dropDownIndicator.classList.toggle('bi-chevron-up');
        dropDownIndicator.classList.toggle('bi-chevron-down');
      }
    })
  });

  /* Scroll top button */
  const scrollTop = document.querySelector('.scroll-top');
  if (scrollTop) {
    const togglescrollTop = function() {
      window.scrollY > 100 ? scrollTop.classList.add('active') : scrollTop.classList.remove('active');
    }
    window.addEventListener('load', togglescrollTop);
    document.addEventListener('scroll', togglescrollTop);
    scrollTop.addEventListener('click', window.scrollTo({
      top: 0,
      behavior: 'smooth'
    }));
  }
});

function popularNavbar(categorias) {
  const navbarUl = document.querySelector('#dynamic-navbar');
  categorias.forEach(categoria => {
    if (categoria.ativo) {
      const li = document.createElement('li');
      const a = document.createElement('a');

      a.href = categoria.url;
      a.textContent = categoria.desc;
      a.setAttribute('data-id', categoria.id);

      // Adicionar evento de clique
      a.addEventListener('click', function(event) {
        event.preventDefault(); // Impede o redirecionamento imediato
        localStorage.setItem('categoriaId', categoria.id);
        localStorage.setItem('categoriaDesc', categoria.desc);
        window.location.href = categoria.url; // Redireciona após salvar o id
      });

      li.appendChild(a);
      navbarUl.appendChild(li);
    }
  });
}

// Função para popular o grid de categorias
function popularGrid(categorias) {
  const categoriesContainer = document.getElementById('categories-container');

  if (categorias.length === 0) {
    categoriesContainer.innerHTML = '<p style="color: rgba(255, 255, 255, 0.6); font-size: 14px;">nenhuma categoria disponível</p>';
    return;
  }

  let count = 0; // Contador para controlar a alternância das classes  
  categorias.forEach(categoria => {
    let colClass;
    
    // Definir a classe com base no valor do contador
    if (count % 3 === 0) {
      colClass = 'col-md-5';
    } else {
      colClass = 'col-md-7';
    }  
    // Incrementar o contador, resetando a cada 3
    count = (count + 1) % 3;

    const categoriaDiv = document.createElement('div');
    categoriaDiv.id = 'cat';
    categoriaDiv.className = colClass;

    const link = document.createElement('a');
    link.href = categoria.url;
    link.dataset.id = categoria.id;
    link.innerHTML = `
      <img src="${categoria.img}" alt="${categoria.desc}">
      <div class="desc">${categoria.desc}</div>
    `;

    // Adicionar evento de clique
    link.addEventListener('click', function(event) {
      event.preventDefault(); // Impede o redirecionamento imediato
      localStorage.setItem('categoriaId', categoria.id);
      localStorage.setItem('categoriaDesc', categoria.desc);
      window.location.href = categoria.url; // Redireciona após salvar o id
    });

    categoriaDiv.appendChild(link);
    categoriesContainer.appendChild(categoriaDiv);
  });
}

async function carregarCategoriaDesc(){
  const desc = localStorage.getItem('categoriaDesc');
  const span = document.getElementById('categoria-desc');

  if (desc) span.textContent = desc.toUpperCase();
  else span.textContent = 'Unknown';  
}

async function carregarFaixas() {
  const categoriaId = localStorage.getItem('categoriaId');
  
  if (categoriaId) 
  {
    getFaixas(categoriaId).then(faixas => {
      const gallery = document.getElementById('gallery');
      gallery.innerHTML = ''; // Limpa o conteúdo existente          

      if (faixas.length === 0) {
        gallery.innerHTML = '<p style="text-align: center; color: rgba(255, 255, 255, 0.6); font-size: 16px;">nenhuma faixa de música disponível</p>';
        return;
      }
    
      const row = document.createElement('div');
      row.className = 'row';
    
      faixas.forEach(faixa => {
        const col = document.createElement('div');
        col.className = 'coluna';
    
        const songDiv = document.createElement('div');
        songDiv.className = 'song';
        songDiv.innerHTML = faixa.link;
    
        col.appendChild(songDiv);
        row.appendChild(col);
      });    
      gallery.appendChild(row);
    });
  } else {
      console.error('Categoria ID não encontrado');
  }
}

async function dropdownCategorias() {
  try {
    const categorias = await getCategorias();

    // Verifica se categorias é um array e não está vazio
    if (Array.isArray(categorias) && categorias.length > 0) 
    {
      const selectCategoria = document.getElementById('categoria');
      selectCategoria.innerHTML = ''; // Limpa o dropdown antes de adicionar novas opções

      categorias.forEach(categoria => {
        const option = document.createElement('option');
        option.value = categoria.id;
        option.textContent = categoria.desc;
        selectCategoria.appendChild(option);
      });
    } else {
      console.warn('A lista de categorias está vazia ou não é um array válido.');
    }
  } catch (error) {
    console.error('Erro ao carregar categorias:', error);
  }
}

async function inserirFaixas(event) {
  event.preventDefault();

  const form = event.target;
  const isValid = form.checkValidity();

  clearErrorMessages(); // Limpa mensagens de erro anteriores

  if (!isValid) {
    showErrorMessages(); // Exibe mensagens de erro
    return;
  }

  const titulo = document.getElementById('titulo').value;
  const artista = document.getElementById('artista').value;
  const link = document.getElementById('link').value.replace(/height="(\d+)"/, 'height="152"');
  const categoriaId = document.getElementById('categoria').value;
  
  let usuarioId = document.getElementById('usuario').value;
  if (!usuarioId) usuarioId = 'aae0b75e-ef82-4ed2-b45e-02de53ab4c61';

  const faixa = {
      titulo: titulo,
      artista: artista,
      link: link,
      categoriaId: parseInt(categoriaId, 10),
      usuarioId: usuarioId
  };

  try {
    const success = await postFaixas(faixa); // Aguarda a resposta da requisição
    if (success) {
      form.reset(); 
    }
  } catch (error) {
    console.error('Erro ao inserir faixa:', error);
  }
}


function clearErrorMessages() {
  const errorMessages = document.querySelectorAll('.error-message');
  errorMessages.forEach(function(message) {
      message.textContent = '';
  });

  const fields = document.querySelectorAll('.form-control');
  fields.forEach(function(field) {
      field.classList.remove('touched');
  });
}

function showErrorMessages() {
  const fields = document.querySelectorAll('.form-control');
  fields.forEach(function(field) {
      if (!field.checkValidity()) {
          const errorMessage = field.parentElement.querySelector('.error-message');
          errorMessage.textContent = 'Este campo é obrigatório.';
          field.classList.add('touched');
      }
  });
}

function addValidationEvents() {
  document.querySelectorAll('.form-control, .form-select').forEach(function(field) {
      field.addEventListener('blur', function() {
          if (!field.checkValidity()) {
              field.classList.add('touched');
              const errorMessage = field.parentElement.querySelector('.error-message');
              errorMessage.textContent = 'Este campo é obrigatório.';
          } else {
              field.classList.remove('touched');
              const errorMessage = field.parentElement.querySelector('.error-message');
              errorMessage.textContent = '';
          }
      });
  });
}

function disableButton(){
  const form = document.getElementById('form-template');
  const submitBtn = document.getElementById('submit-btn');
  const requiredInputs = form.querySelectorAll('input[required], textarea[required]');

  function checkRequiredFields() {
      let allFilled = true;
      requiredInputs.forEach(input => {
          if (input.value.trim() === "") {
              allFilled = false;
          }
      });
      submitBtn.disabled = !allFilled;
  }

  requiredInputs.forEach(input => {
      input.addEventListener('input', checkRequiredFields);
  });

  // Initial check in case fields are pre-filled
  checkRequiredFields();
}
