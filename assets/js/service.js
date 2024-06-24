
async function getFaixas(categoriaId) {
  try {
    // Simulação de uma requisição assíncrona
    await new Promise(resolve => setTimeout(resolve, 1000));

    const url = `https://localhost:5000/api/faixas/por-categoria/${categoriaId}`;

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

async function getCategorias() {
    try {
      const response = await fetch('https://localhost:5000/api/categorias');
      const result = await response.json();

      if (result.success) {
        result.data.forEach(categoria => {
          if (categoria.url != "#") categoria.url = categoria.url + '.html';
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
