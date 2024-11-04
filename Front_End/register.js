document.getElementById('registerForm').addEventListener('submit', function(e) {
    e.preventDefault();

    const role = document.getElementById('role').value;
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const nic = document.getElementById('nic').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    if (password !== confirmPassword) {
        alert("Passwords do not match!");
        return;
    }

    const user = {
        role,
        firstName,
        lastName,
        nic,
        password,
        registeredDate: new Date().toLocaleDateString(),
    };

    let users = JSON.parse(localStorage.getItem('members')) || [];
    users.push(user);
    localStorage.setItem('members', JSON.stringify(users));

    alert('Registration successful!');

    if (role === 'admin') {
        window.location.href = 'login.html'; 
    } else {
        window.location.href = 'login.html';
    }
});