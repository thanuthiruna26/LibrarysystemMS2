function displayBorrowingReport() {
    const borrowingTable = document.querySelector('#borrowingTable tbody');
    let borrowedBooks = JSON.parse(localStorage.getItem('borrowedBooks')) || [];
    let books = JSON.parse(localStorage.getItem('books')) || [];

    borrowingTable.innerHTML = '';

    borrowedBooks.forEach(borrow => {
        // Find the corresponding book details
        const bookDetails = books.find(book => book.name === borrow.bookName);

        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${borrow.nic}</td>
            <td>${borrow.bookName}</td>
            <td><img src="${bookDetails.coverUrl}" alt="${borrow.bookName}" style="width: 100px; height: auto;"></td>
        `;
        borrowingTable.appendChild(row);
    });
}

window.onload = displayBorrowingReport;