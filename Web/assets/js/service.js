
async function getCategorias() {
    try {
      const response = await fetch('https://localhost:5000/api/categorias');
      const result = await response.json();
      if (result.success) {
        // Itera sobre os dados para ajustar o caminho da imagem
      result.data.forEach(category => {
        // Ajuste da URL da imagem para usar o caminho relativo
        category.img = `${category.img}`;
      });

        return result.data;
      } else {
        console.error('Erro ao buscar categorias:', result.message);
        return [];
      }
    } catch (error) {
      console.error('Erro ao fazer a requisição:', error);
      return [];
    }
}

function construirNavbar(categorias) {
  const navbarUl = document.querySelector('#navbar .dropdown ul');
  categorias.forEach(categoria => {
      if (categoria.ativo) {
      const li = document.createElement('li');
      const a = document.createElement('a');
      a.href = categoria.url + ".html";
      a.textContent = categoria.desc;
      li.appendChild(a);
      navbarUl.appendChild(li);
      }
  });
}

// Função para popular o grid de categorias
function popularGrid(categorias) {
  const categoriesContainer = document.getElementById('categories-container');

  if (categorias.length === 0) {
    categoriesContainer.innerHTML = '<p>Nenhuma categoria encontrada.</p>';
    return;
  }

  let count = 0; // Contador para controlar a alternância das classes

  categorias.forEach(category => {
    let colClass;
    
    // Definir a classe com base no valor do contador
    if (count % 3 === 0) {
      colClass = 'col-md-5';
    } else {
      colClass = 'col-md-7';
    }

    // Incrementar o contador, resetando a cada 3
    count = (count + 1) % 3;

    const categoryDiv = document.createElement('div');
    categoryDiv.id = 'cat';
    categoryDiv.className = colClass;

    categoryDiv.innerHTML = `
      <a href="${category.url}.html">
        <img src="${category.img}" alt="${category.desc}">
        <div class="desc">${category.desc}</div>
      </a>
    `;
    categoriesContainer.appendChild(categoryDiv);
  });
}

// Função de inicialização
async function init() {
    const categorias = await getCategorias();
    construirNavbar(categorias);
    popularGrid(categorias);
}

// Chamando a função init ao carregar a página
document.addEventListener('DOMContentLoaded', init);