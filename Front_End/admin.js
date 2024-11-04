document.getElementById('addBookForm').addEventListener('submit', function(e) {
    e.preventDefault();

    const book = {
        name: document.getElementById('bookName').value,
        isbn: parseInt(document.getElementById('isbn').value),
        publisher: document.getElementById('publisher').value,
        copies: parseInt(document.getElementById('copies').value, 10),
        coverUrl: document.getElementById('coverUrl').value,
        genre: document.getElementById('genre').value,
    };

    console.log(book)

    document.getElementById("addBook").addEventListener("click",addBook );
    document.getElementById("addmember").addEventListener("click",addMember);

   // Function to add a book via Fetch API
   let bookurl= "http://localhost:5247/api/Books";
   let memberurl = "http://localhost:5247/api/Member";
async function addBook() {
    
    try {
        // Send the book to the server
        const response = await fetch(bookurl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(book)
        });

        // Check if the response is successful
        if (!response.ok) {
            throw new Error('Failed to add book');
        }

        // Alert the user and reset the form
        alert('Book added successfully!');
        document.getElementById('addBookForm').reset();

        // Fetch the updated list of books
        displayBooks();
    } catch (error) {
        console.error(error);
        alert('There was an error adding the book.');
    }
}

// Function to get and display the list of books
async function displayBooks() {
    try {
        // Fetch the list of books from the server
        const response = await fetch(bookurl);
        
        if (!response.ok) {
            throw new Error('Failed to fetch books');
        }

        const books = await response.json();

        // Assuming there's an element with id 'bookList' to display the books
        const bookList = document.getElementById('bookList');
        bookList.innerHTML = ''; // Clear previous books

        // Render the list of books
        books.forEach(book => {
            const bookItem = document.createElement('li');
            bookItem.textContent = `${book.title} by ${book.author}`;
            bookList.appendChild(bookItem);
        });
    } catch (error) {
        console.error(error);
        alert('There was an error fetching the books.');
    }
}

// Call the displayBooks function when the page loads
document.addEventListener('DOMContentLoaded', () => {
    displayBooks();
});

});

document.getElementById('addMemberForm').addEventListener('submit', function(e) {
    e.preventDefault();

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
        firstName,
        lastName,
        nic,
        password, // Save the password in local storage
        registeredDate: new Date().toLocaleDateString(),
    };

   // Function to add a new member via Fetch API
async function addMember(user) {
    try {
        // Send the user data to the server using a POST request
        const response = await fetch(memberurl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });

        // Check if the response is successful
        if (!response.ok) {
            throw new Error('Failed to add member');
        }

        // Alert the user and reset the form
        alert('Member added successfully!');
        document.getElementById('addMemberForm').reset();

        // Fetch the updated list of members
        displayMembers();
    } catch (error) {
        console.error(error);
        alert('There was an error adding the member.');
    }
}

// Function to fetch and display the list of members
async function displayMembers() {
    try {
        // Fetch the list of members from the server using a GET request
        const response = await fetch(memberurl);

        if (!response.ok) {
            throw new Error('Failed to fetch members');
        }

        const members = await response.json();

        // Assuming there's an element with id 'memberList' to display the members
        const memberList = document.getElementById('memberList');
        memberList.innerHTML = ''; // Clear previous members

        // Render the list of members
        members.forEach(member => {
            const memberItem = document.createElement('li');
            memberItem.textContent = `${member.name}, ${member.email}`;
            memberList.appendChild(memberItem);
        });
    } catch (error) {
        console.error(error);
        alert('There was an error fetching the members.');
    }
}

// Call the displayMembers function when the page loads
document.addEventListener('DOMContentLoaded', () => {
    displayMembers();
});

});

function displayBooks() {
    const booksTableBody = document.getElementById('booksTable').getElementsByTagName('tbody')[0];
    booksTableBody.innerHTML = '';

    let books = JSON.parse(localStorage.getItem('books')) || [];
    let cellNo = 1
    books.forEach((book) => {

        let row = booksTableBody.insertRow();
        let cellnum = row.insertCell(0)
        let cellName = row.insertCell(1);
        let cellISBN = row.insertCell(2);
        let cellPublisher = row.insertCell(3);
        let cellCopies = row.insertCell(4);
        let cellGenre = row.insertCell(5);
        let cellCover = row.insertCell(6);
        let deleteButton = row.insertCell(7);
        cellName.textContent = book.name;
        cellISBN.textContent = book.isbn;
        cellPublisher.textContent = book.publisher;
        cellCopies.textContent = book.copies;
        cellGenre.textContent = book.genre;
        cellnum.textContent = cellNo;
   
       deleteButton.textContent = "delete";
        deleteButton.setAttribute('data-index' ,book.isbn )
        deleteButton.addEventListener('click' , (event) => deleteBook(event));
        deleteButton.setAttribute('class','delete-button' )
        cellnum.setAttribute('id' , book.isbn);
        row.setAttribute('id' ,cellNo);

        let img = document.createElement('img');
        img.src = book.coverUrl;
        cellCover.appendChild(img);
        cellNo++;
    });
}

function displayMembers() {
    const membersTableBody = document.getElementById('membersTable').getElementsByTagName('tbody')[0];
    membersTableBody.innerHTML = '';

    let members = JSON.parse(localStorage.getItem('members')) || [];

    members.forEach((member) => {
        let row = membersTableBody.insertRow();

        let cellFirstName = row.insertCell(0);
        let cellLastName = row.insertCell(1);
        let cellNIC = row.insertCell(2);
        let cellDate = row.insertCell(3);

        cellFirstName.textContent = member.firstName;
        cellLastName.textContent = member.lastName;
        cellNIC.textContent = member.nic;
        cellDate.textContent = member.registeredDate;
    });
}

function deleteBook(event) {
    let bookISBN = event.target.getAttribute("data-index");
    let books = JSON.parse(localStorage.getItem('books')) || [];

    // Find the book in the array
    let findBook = books.find(b => b.isbn == bookISBN);
    let findbookIndex = books.indexOf(findBook);

    // Check if the book was found
    if (findbookIndex !== -1) {
        // Remove the book from the array
        books.splice(findbookIndex, 1);
        localStorage.setItem('books', JSON.stringify(books));

        let booksTable = document.getElementById('booksTable');
        
        // Adjust the row index for deletion (add 1 to account for the header)
        let row = findbookIndex + 1;
        booksTable.deleteRow(row);
    }
}


// Initial display of books and members
displayBooks();
displayMembers();
