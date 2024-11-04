function displayMemberReport() {
    const memberTable = document.querySelector('#memberTable tbody');
    let members = JSON.parse(localStorage.getItem('members')) || [];
    let books = JSON.parse(localStorage.getItem('books')) || [];
    let borrowedBooks = JSON.parse(localStorage.getItem('borrowedBooks')) || [];

    memberTable.innerHTML = '';

    members.forEach(member => {
        // Get borrowed books for the member
        let borrowedForMember = borrowedBooks.filter(b => b.nic === member.nic);
        
        borrowedForMember.forEach(borrowed => {
            // Find the corresponding book details
            const bookDetails = books.find(book => book.name === borrowed.bookName);
            
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${member.firstName}</td>
                <td>${member.nic}</td>
                <td>${borrowed.bookName}</td>
                <td><img src="${bookDetails.coverUrl}" alt="${borrowed.bookName}" style="width: 100px; height: auto;"></td>
            `;
            memberTable.appendChild(row);
        });
    });
}

window.onload = displayMemberReport;