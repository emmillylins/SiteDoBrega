async function login(event) {
    event.preventDefault();
  
    const form = event.target;
    const isValid = form.checkValidity();
  
    clearErrorMessages(); // Limpa mensagens de erro anteriores
  
    if (!isValid) {
      showErrorMessages(); // Exibe mensagens de erro
      return;
    }
  
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
  
    try {
        const response = await fetch('https://localhost:5000/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
        });

        const data = await response.json();

        if (!response.ok || !data.success) {
            throw new Error('Credenciais inválidas');
        }

        // Armazena o token no localStorage
        localStorage.setItem('token', data.data.token);
        localStorage.setItem('userId', data.data.userId);
        localStorage.setItem('username', data.data.username);
        
        alert('Login realizado com sucesso!');
        window.location.href = 'cadastro.html'; // Redireciona para a página protegida
    } catch (error) {
        console.error('Erro no login:', error);
        alert('Falha no login. Verifique suas credenciais.');
    }
  }

async function fetchWithAuth(url, options = {}) {
    const token = localStorage.getItem('token');

    if (!token) {
        alert('Você precisa fazer login!');
        window.location.href = 'login.html'; // Redireciona para login
        return;
    }

    // Garante que os headers existam
    const headers = {
        ...options.headers, // Mantém os headers que vierem na chamada
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
    };

    try {
        const response = await fetch(url, { ...options, headers });

        if (response.status === 401) { 
            alert('Sessão expirada. Faça login novamente.');
            logout();
            return; 
        }

        return await response.json(); // Retorna o JSON processado
    } catch (error) {
        console.error('Erro na requisição:', error);
        throw error; // Lança o erro para ser tratado onde a função foi chamada
    }
}

//Sempre que um usuário acessar uma página protegida, verificamos se ele tem um token válido.
function checkAuth() {
    const token = localStorage.getItem('token');

    if (!token) {
        window.location.href = 'login.html'; // Redireciona para o login
    }
}
