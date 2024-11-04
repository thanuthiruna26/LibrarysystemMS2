function displayInventoryReport() {
    const inventoryTable = document.querySelector('#inventoryTable tbody');
    const bookCardContainer = document.getElementById('bookCardContainer');
    let books = JSON.parse(localStorage.getItem('books')) || [];

    // Clear existing content
    inventoryTable.innerHTML = '';
    bookCardContainer.innerHTML = '';

    books.forEach(book => {
        // Add book data to the table
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${book.name}</td>
            <td>${book.isbn}</td>
            <td>${book.publisher}</td>
            <td>${book.genre}</td>
            <td>${book.copies}</td>
        `;
        inventoryTable.appendChild(row);

        // Create and add book cards
        const bookCard = document.createElement('div');
        bookCard.classList.add('book-card');
        bookCard.innerHTML = `
            <img src="${book.coverUrl}" alt="${book.name}">
            <h3>${book.name}</h3>
            <p>ISBN: ${book.isbn}</p>
            <p>Publisher: ${book.publisher}</p>
            <p>Genre: ${book.genre}</p>
            <p>Copies Available: ${book.copies}</p>
        `;
        bookCardContainer.appendChild(bookCard);
    });
}

window.onload = displayInventoryReport;