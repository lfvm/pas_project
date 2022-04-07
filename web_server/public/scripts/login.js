//Scripts para manejar la pantalla del login 



const handleLogin = async(e) => {

    e.preventDefault();

    const loginForm = document.getElementById('login_form');

    const email = loginForm[0].value;
    const password = loginForm[1].value;

    if (email === '' || password === '') {
        return alert('Please fill in all fields');
    }

    


    //TODO: Enviar los datos al servidor para validar, en caso de ser correctos ir a home, de lo contrario mostrar mensaje de error
    

    //Guardar los datos en el localStorage en caso de que el login sea exitoso
    localStorage.setItem('mail', email);

    //Redirigir a home
    window.location.href = '/';

}




const handleCreateAccount = async(e) => {

    e.preventDefault();
    const form = document.getElementById('create_account_form');

    //Obtener los datos del formulario
    const data = {
        correo: form[0].value,
        password: form[1].value,
        nombre: form[2].value,
        apellidos: form[3].value,
        gender: form[4].value,
        instrumento: form[5].value,
        edad: form[6].value
    }
    


    fetch('http://localhost:8080/api/usuarios/nuevo', {
        method: "POST",
        body: JSON.stringify(data),
        headers: {"Content-type": "application/json; charset=UTF-8"}
    })
        .then((response) => { 
            console.log(response.json())
        }) 
        .catch(err => console.log(err));



}