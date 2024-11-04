function displayBorrowedBooks() {
    const returnTable = document.querySelector('#returnTable tbody');
    let borrowedBooks = JSON.parse(localStorage.getItem('borrowedBooks')) || [];
    let books = JSON.parse(localStorage.getItem('books')) || [];
    let members = JSON.parse(localStorage.getItem('members')) || [];

    returnTable.innerHTML = '';

    borrowedBooks.forEach((borrow, index) => {
        // Find the corresponding book details
        const bookDetails = books.find(book => book.name === borrow.bookName);
        // Find the corresponding member details
        const memberDetails = members.find(member => member.nic === borrow.nic);

        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${memberDetails.firstName}</td>
            <td>${memberDetails.nic}</td>
            <td>${borrow.bookName}</td>
            <td>${bookDetails.isbn}</td>
            <td><img src="${bookDetails.coverUrl}" alt="${borrow.bookName}" style="width: 100px; height: auto;"></td>
            <td><button onclick="processReturn(${index})">Process Return</button></td>
        `;
        returnTable.appendChild(row);
    });
}

function processReturn(index) {
    let borrowedBooks = JSON.parse(localStorage.getItem('borrowedBooks')) || [];
    const returnedBook = borrowedBooks[index];

    let books = JSON.parse(localStorage.getItem('books')) || [];
    books = books.map(book => {
        if (book.name === returnedBook.bookName) {
            book.copies++;
        }
        return book;
    });
    localStorage.setItem('books', JSON.stringify(books));

    borrowedBooks.splice(index, 1);
    localStorage.setItem('borrowedBooks', JSON.stringify(borrowedBooks));

    displayBorrowedBooks();
}

window.onload = displayBorrowedBooks;