
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



  if (desc) {
      span.textContent = desc.toUpperCase();
  } else {
      span.textContent = 'Unknown';
  }  
}

async function carregarFaixas() {
  const categoriaId = localStorage.getItem('categoriaId');
  if (categoriaId) {

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
      const categorias = await getCategorias(); // Aguarda a Promise ser resolvida

      const selectCategoria = document.getElementById('categoria');
      selectCategoria.innerHTML = ''; // Limpa o dropdown antes de adicionar novas opções

      categorias.forEach(categoria => {
          const option = document.createElement('option');
          option.value = categoria.id;
          option.textContent = categoria.desc;
          selectCategoria.appendChild(option);
      });
  } catch (error) {
      console.error('Erro ao carregar categorias:', error);
  }
}


//novo form
function clonarForm() {
  var formTemplate = document.getElementById('form-template');
  var newForm = formTemplate.cloneNode(true);

  // Clear the input values in the cloned form
  var inputs = newForm.getElementsByTagName('input');
  for (var i = 0; i < inputs.length; i++) {
      inputs[i].value = '';
  }
  var textareas = newForm.getElementsByTagName('textarea');
  for (var i = 0; i < textareas.length; i++) {
      textareas[i].value = '';
  }
  var selects = newForm.getElementsByTagName('select');
  for (var i = 0; i < selects.length; i++) {
      selects[i].selectedIndex = 0;
  }
  
  // Esconder botões do formulário superior se já não estiverem escondidos
  var currentForm = document.querySelector('.form-container');
  var buttons = currentForm.querySelector('.botao');
  if (buttons) {
      buttons.style.display = 'none';
  }

  // Append the cloned form to the section
  document.getElementById('cadastro').appendChild(newForm);
}
