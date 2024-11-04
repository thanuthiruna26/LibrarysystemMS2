document.getElementById('loginForm').addEventListener('submit', function(e) {
    e.preventDefault();

    const nic = document.getElementById('loginNic').value;
    const password = document.getElementById('loginPassword').value;

    let members = JSON.parse(localStorage.getItem('members')) || [];
    let member = members.find(u => u.nic === nic && u.password === password);

    if (member) {
        localStorage.setItem('loggedInMember', JSON.stringify(member));
        alert('Login successful!');

        if (member.role === 'admin') {
            window.location.href = 'admin.html'; // Redirect to admin index page
        } else {
            window.location.href = 'gallery.html'; // Redirect to member gallery page
        }
    } else {
        alert('Invalid NIC or password.');
    }
});