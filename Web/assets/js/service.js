
async function getFaixas(categoriaId) {
  try {
    // Simulação de uma requisição assíncrona
    await new Promise(resolve => setTimeout(resolve, 1000));

    const url = `https://localhost:5000/api/faixas/${categoriaId}`;

    const response = await fetch(url);
    const result = await response.json();
    
    if (result.success) {
      result.data.forEach(faixa => {
        faixa.link = faixa.link.replace(/'/g, `"`);
      });
      return result.data;
    } 
    else {
      console.error('Erro ao buscar Faixas:', result.message);
      return [];
    }

  } catch (error) {
    console.error('Erro ao fazer a requisição:', error);
    return [];
  }
}

async function getCategoriaById(id) {
    try {
      const url = `https://localhost:5000/api/categorias/${id}`
      const response = await fetch(url);
      const result = await response.json();

      if (result.success) {
        // if (result.data.url != "#") result.data.url = result.data.url + '.html';
        if (result.data.url != "#") result.data.url = 'categoria.html'
        return result.data;
      } 
      else {
        console.error('Erro ao buscar categorias:', result.message);
        return [];
      }
    } 
    catch (error) {
      console.error('Erro ao fazer a requisição:', error);
      return [];
    }
}

async function getCategorias() {
    try {
      const response = await fetch('https://localhost:5000/api/categorias');
      const result = await response.json();

      if (result.success) {
        result.data.forEach(categoria => {
          // if (categoria.url != "#") categoria.url = categoria.url + '.html';
          if (categoria.url != "#") categoria.url = 'categoria.html'
        });
        return result.data;
      } 
      else {
        console.error('Erro ao buscar categorias:', result.message);
        return [];
      }
    } 
    catch (error) {
      console.error('Erro ao fazer a requisição:', error);
      return [];
    }
}

async function postFaixas(faixa) {
  try {
    const response = await fetch('https://localhost:5000/api/faixas', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(faixa)
    });

    const data = await response.json();
    console.log(data);

    if (data.success) {
      alert('Inserção bem sucedida!');
      return true;
    } else {
      alert(JSON.stringify(data.errors));
      return false;
    }
  } catch (error) {
    alert('Erro: ' + error);
    return false;
  }
}