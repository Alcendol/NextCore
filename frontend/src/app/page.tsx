import Image from 'next/image';
import Link from 'next/link';

export default function Home() {
  return (
    <section id="Home" className="relative min-h-screen overflow-hidden">
      {/* Background Image */}
      <div className="absolute inset-0">
        <Image 
          src="/tamanbaca.jpg" 
          layout="fill" 
          objectFit="cover" 
          alt="Background Image"
        />
      </div>

      <div className="absolute inset-0 bg-blue-900 to-transparent opacity-95 z-5"></div>

      {/* Content */}
      <div className="relative z-10 flex items-center justify-center h-full py-[15rem]">
        <div className="container mx-auto px-4 lg:px-8">
          <div className="flex flex-wrap">
            <div className="w-full px-2 flex justify-center">
              <div className="relative mb-10">
                <span className="font-sans text-white text-5xl font-bold">Welcome to Rakku</span>
              </div>
            </div>
            <div className="w-full px-2 flex items-center justify-center mb-10">
              <div className="text-center">
                <span className="text-white font-sans text-center text-lg">
                  Selamat datang di Rakku! Rakku adalah aplikasi yang dibuat untuk memproses peminjaman buku supaya lebih mudah dan efisien di Taman Bacaan Masyarakat (TBM). Rakku hadir sebagai solusi peningkatan efektivitas pengelolaan buku untuk memaksimalkan pelayanan kepada para pembaca, sekaligus membantu mendukung pengembangan literasi masyarakat. Di Rakku, kamu bisa meminjam sekaligus mengembalikan buku yang kamu pinjam di satu aplikasi yang sama, sehingga pendataan buku yang ada akan lebih praktis.
                </span>
              </div>
            </div>
            <div className="w-full px-2 flex items-center justify-center mb-10">
              <div className="text-center">
                <span className="text-white font-sans text-center text-lg">
                  Rakku juga sangat terbuka untuk pengembangan literasi berbasis teknologi seperti pengadaan e-book dan audiobook. Fitur-fitur ini masih dalam tahap eksplorasi, namun Rakku berkomitmen untuk terus berkembang dan berinovasi agar dapat menjadi platform literasi yang relevan di era digital.
                </span>
              </div>
            </div>
            <div className="w-full px-2 flex items-center justify-center">
              <div className="text-center">
                <Link href="/signin">
                  <button className="w-full bg-white text-blue-900 hover:bg-gray-300 h-10 p-6 flex items-center justify-center">
                    Go to sign in
                  </button>
                </Link>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Bottom-right Image */}
      <div className="absolute bottom-0 right-0 z-10">
        <Image 
          src="/page_7.png" 
          width={500} 
          height={500} 
          alt="Corner Image"
        />
      </div>
    </section>
  );
}





// 'use client';  // This is necessary to mark this component as a client component

// import { useEffect, useState } from 'react';
// import Card from '@/components/card';

// interface Book {
//   bookId: string;
//   title: string;
//   datePublished: string;
//   totalPage: number;
//   country: string;
//   language: string;
//   genre: string;
//   desc: string;
// }

// const Home: React.FC = () => {
//   const [books, setBooks] = useState<Book[]>([]);
//   const [loading, setLoading] = useState<boolean>(true); // To track loading state
//   const [error, setError] = useState<string | null>(null); // To track error state

//   useEffect(() => {
//     fetch('http://localhost:5259/api/book')
//       .then((response) => {
//         if (!response.ok) {
//           console.error('Error fetching data:', response.statusText);
//           throw new Error('Network response was not ok');
//         }
//         return response.json();
//       })
//       .then((data) => {
//         console.log('Fetched data:', data); // Log the data to inspect it
//         setBooks(data);
//         setLoading(false);
//       })
//       .catch((error) => {
//         console.error('Error fetching data:', error);
//         setError('Failed to fetch data. Please try again later.');
//         setLoading(false);
//       });
//   }, []);

//   return (
//     <div className="my-10">
//       <h1 className="text-xl font-bold mb-4">Books</h1>
//       {loading && <p>Loading...</p>}
//       {error && <p style={{ color: 'red' }}>{error}</p>}
//       <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
//         {books.map((book) => (
//           <Card
//             key={book.bookId}
//             title={book.title}
//             desc={book.desc}
//             genre={book.genre}
//             datePublished={book.datePublished}
//           />
//         ))}
//       </div>
//     </div>
//   );
// };

// export default Home;



// 'use client';
// import React, { useState } from "react";

// // Define types for the borrow response
// interface Borrow {
//   borrowId: string;
//   userId: string;
//   borrowDate: string;
//   returnDate: string;
//   pending: number;
//   borrowed: number;
//   returned: number;
// }

// interface BorrowedBook {
//   borrowId: string;
//   bookId: string;
//   title: string;
//   authorName: string;
//   returnDate: string | null;
//   status: "Pending" | "Borrowed" | "Returned" | "Overdue";
// }

// const BorrowTest: React.FC = () => {
//   const [userId, setUserId] = useState<string>("");
//   const [borrowList, setBorrowList] = useState<Borrow[]>([]);
//   const [borrowedBooks, setBorrowedBooks] = useState<BorrowedBook[]>([]);
//   const [error, setError] = useState<string>("");

//   // Fetch borrow history for a user
//   const fetchBorrowHistory = async () => {
//     if (!userId) {
//       setError("Please provide a userId");
//       return;
//     }

//     try {
//       const response = await fetch(`http://localhost:5259/api/borrow/user123`);
//       if (!response.ok) {
//         throw new Error("Failed to fetch borrow history");
//       }
//       const data: Borrow[] = await response.json();
//       setBorrowList(data);
//       setError("");
//     } catch (err) {
//       setError((err as Error).message);
//     }
//   };

//   // Fetch borrowed books for a specific borrowId
//   const fetchBorrowedBooks = async (borrowId: string) => {
//     try {
//       const response = await fetch(`http://localhost:5259/api/borrow/borrowed-books/${borrowId}`);
//       if (!response.ok) {
//         throw new Error("Failed to fetch borrowed books");
//       }
//       const data: BorrowedBook[] = await response.json();
//       setBorrowedBooks(data);
//       setError("");
//     } catch (err) {
//       setError((err as Error).message);
//     }
//   };

//   return (
//     <div>
//       <h1>Borrowed Books Test</h1>

//       {/* User ID input */}
//       <div>
//         <label htmlFor="userId">Enter User ID: </label>
//         <input
//           id="userId"
//           type="text"
//           value={userId}
//           onChange={(e) => setUserId(e.target.value)}
//         />
//         <button onClick={fetchBorrowHistory}>Fetch Borrow History</button>
//       </div>

//       {/* Error handling */}
//       {error && <p style={{ color: "red" }}>{error}</p>}

//       {/* Borrow history table */}
//       {borrowList.length > 0 && (
//         <div>
//           <h2>Borrow History</h2>
//           <table>
//             <thead>
//               <tr>
//                 <th>Borrow ID</th>
//                 <th>Borrow Date</th>
//                 <th>Return Date</th>
//                 <th>Pending</th>
//                 <th>Borrowed</th>
//                 <th>Returned</th>
//               </tr>
//             </thead>
//             <tbody>
//               {borrowList.map((borrow) => (
//                 <tr key={borrow.borrowId}>
//                   <td>{borrow.borrowId}</td>
//                   <td>{new Date(borrow.borrowDate).toLocaleDateString()}</td>
//                   <td>{new Date(borrow.returnDate).toLocaleDateString()}</td>
//                   <td>{borrow.pending}</td>
//                   <td>{borrow.borrowed}</td>
//                   <td>{borrow.returned}</td>
//                   <td>
//                     <button onClick={() => fetchBorrowedBooks(borrow.borrowId)}>
//                       Fetch Borrowed Books
//                     </button>
//                   </td>
//                 </tr>
//               ))}
//             </tbody>
//           </table>
//         </div>
//       )}

//       {/* Borrowed books table */}
//       {borrowedBooks.length > 0 && (
//         <div>
//           <h2>Borrowed Books</h2>
//           <table>
//             <thead>
//               <tr>
//                 <th>Book Title</th>
//                 <th>Author</th>
//                 <th>Return Date</th>
//                 <th>Status</th>
//               </tr>
//             </thead>
//             <tbody>
//               {borrowedBooks.map((book) => (
//                 <tr key={book.bookId}>
//                   <td>{book.title}</td>
//                   <td>{book.authorName}</td>
//                   <td>{book.returnDate ? new Date(book.returnDate).toLocaleDateString() : "Not Returned"}</td>
//                   <td>{book.status}</td>
//                 </tr>
//               ))}
//             </tbody>
//           </table>
//         </div>
//       )}
//     </div>
//   );
// };

// export default BorrowTest;


// 'use client';
// import { useState } from 'react';

// const BorrowPage = () => {
//   const [userId, setUserId] = useState('');
//   const [borrowId, setBorrowId] = useState('');
//   const [bookId, setBookId] = useState('');
//   const [borrowData, setBorrowData] = useState<borrow[]>([]);
//   const [borrowedBooks, setBorrowedBooks] = useState<book[]>([]);
//   const [message, setMessage] = useState('');

//   interface borrow {
//     borrowId: string;
//     userId: string;
//     borrowDate: string;
//     returnDate: string;
//     pendingBooks: number;
//     borrowedBooks: number;
//     returnedBooks: number;
//   }

//   interface book{
//     borrowId: string;
//     bookId: string;
//     title: string;
//     authorName: string;
//     returnDate: string;
//     status: string;
//   }

//   const handleFetchBorrowHistory = async () => {
//     try {
//       const response = await fetch(`http://localhost:5259/api/borrow/${userId}`);
//       if (!response.ok) {
//         throw new Error('Failed to fetch borrow history.');
//       }
//       const data = await response.json();
//       setBorrowData(data);
//       setMessage('');
//     } catch (err) {
//       setMessage((err as Error).message);
//     }
//   };

//   const handleFetchBorrowedBooks = async () => {
//     try {
//       const response = await fetch(`http://localhost:5259/api/borrow/borrowed-books/${borrowId}`);
//       if (!response.ok) {
//         throw new Error('Failed to fetch borrowed books.');
//       }
//       const data = await response.json();
//       setBorrowedBooks(data);
//       setMessage('');
//     } catch (err) {
//       setMessage((err as Error).message);
//     }
//   };

//   const handleCancelBorrow = async () => {
//     try {
//       const response = await fetch(`http://localhost:5259/api/borrow/cancel-borrow/${borrowId}`, {
//         method: 'DELETE',
//       });
//       if (!response.ok) {
//         throw new Error('Failed to cancel borrow order.');
//       }
//       const data = await response.json();
//       setMessage(data.Message || 'Borrow order canceled successfully.');
//     } catch (err) {
//       setMessage((err as Error).message);
//     }
//   };

//   const handleCancelBook = async () => {
//     try {
//       const response = await fetch(`http://localhost:5259/api/borrow/${borrowId}/${bookId}`, {
//         method: 'DELETE',
//       });
//       if (!response.ok) {
//         throw new Error('Failed to cancel book.');
//       }
//       const data = await response.json();
//       setMessage(data.Message || 'Book canceled successfully.');
//     } catch (err) {
//       setMessage((err as Error).message);
//     }
//   };

//   return (
//     <div style={{ padding: '20px' }}>
//       <h1>Borrow API Test</h1>
//       {message && <p style={{ color: 'red' }}>{message}</p>}

//       {/* Fetch Borrow History */}
//       <div>
//         <h2>Fetch Borrow History</h2>
//         <input
//           type="text"
//           placeholder="User ID"
//           value={userId}
//           onChange={(e) => setUserId(e.target.value)}
//         />
//         <button onClick={handleFetchBorrowHistory}>Fetch History</button>
//         {borrowData.length > 0 && (
//           <ul>
//             {borrowData.map((borrow) => (
//               <li key={borrow.borrowId}>
//                 Borrow ID: {borrow.borrowId}, Pending: {borrow.pendingBooks}, Borrowed: {borrow.borrowedBooks}, Returned: {borrow.returnedBooks}
//               </li>
//             ))}
//           </ul>
//         )}
//       </div>

//       {/* Fetch Borrowed Books */}
//       <div>
//         <h2>Fetch Borrowed Books</h2>
//         <input
//           type="text"
//           placeholder="Borrow ID"
//           value={borrowId}
//           onChange={(e) => setBorrowId(e.target.value)}
//         />
//         <button onClick={handleFetchBorrowedBooks}>Fetch Books</button>
//         {borrowedBooks.length > 0 && (
//           <ul>
//             {borrowedBooks.map((book) => (
//               <li key={book.bookId}>
//                 Book ID: {book.bookId}, Title: {book.title}, Author: {book.authorName}, Status: {book.status}
//               </li>
//             ))}
//           </ul>
//         )}
//       </div>

//       {/* Cancel Borrow Order */}
//       <div>
//         <h2>Cancel Borrow Order</h2>
//         <input
//           type="text"
//           placeholder="Borrow ID"
//           value={borrowId}
//           onChange={(e) => setBorrowId(e.target.value)}
//         />
//         <button onClick={handleCancelBorrow}>Cancel Order</button>
//       </div>

//       {/* Cancel Specific Book */}
//       <div>
//         <h2>Cancel Book</h2>
//         <input
//           type="text"
//           placeholder="Borrow ID"
//           value={borrowId}
//           onChange={(e) => setBorrowId(e.target.value)}
//         />
//         <input
//           type="text"
//           placeholder="Book ID"
//           value={bookId}
//           onChange={(e) => setBookId(e.target.value)}
//         />
//         <button onClick={handleCancelBook}>Cancel Book</button>
//       </div>
//     </div>
//   );
// };

// export default BorrowPage;

// 'use client';
// import React, { useState } from "react";

// // Define types for the API request and response
// type BorrowRequestDTO = {
//   userId: string;
//   borrowDate: string;
//   returnDate: string;
//   bookList: string[];
// };

// type BorrowBooksResponse = {
//   message?: string;
//   [key: string]: unknown; // For additional properties from the server
// };

// const BorrowBooksPage: React.FC = () => {
//   const [userId, setUserId] = useState("");
//   const [borrowDate, setBorrowDate] = useState("");
//   const [returnDate, setReturnDate] = useState("");
//   const [bookList, setBookList] = useState([{ bookId: "" }]);
//   const [statusMessage, setStatusMessage] = useState<string | null>(null);

//   const handleBookChange = (index: number, value: string) => {
//     const updatedBooks = [...bookList];
//     updatedBooks[index].bookId = value;
//     setBookList(updatedBooks);
//   };

//   const addBookField = () => {
//     setBookList([...bookList, { bookId: "" }]);
//   };

//   const removeBookField = (index: number) => {
//     const updatedBooks = bookList.filter((_, i) => i !== index);
//     setBookList(updatedBooks);
//   };

//   const validateForm = () => {
//     // Ensure all fields are filled in
//     if (!userId || !borrowDate || !returnDate || bookList.some((book) => !book.bookId)) {
//       setStatusMessage("Please fill in all the fields and add valid books.");
//       return false;
//     }
//     return true;
//   };

//   const handleBorrowBooks = async () => {
//     if (!validateForm()) return;
  
//     // Convert dates to string format (YYYY-MM-DD)
//     const formatDate = (date: string) => {
//       const d = new Date(date);
//       return d.toISOString().split('T')[0]; // Extract only the date part (YYYY-MM-DD)
//     };
  
//     const requestPayload: BorrowRequestDTO = {
//       userId,
//       borrowDate: formatDate(borrowDate),  // Format date as string
//       returnDate: formatDate(returnDate),  // Format date as string
//       bookList: bookList.map(book => book.bookId)
//     };
  
//     console.log("Request Payload:", JSON.stringify(requestPayload, null, 2)); // Log the request payload
  
//     try {
//       const response = await fetch(`http://localhost:5259/api/borrow/${userId}`, {
//         method: "POST",
//         headers: {
//           "Content-Type": "application/json",
//         },
//         body: JSON.stringify(requestPayload),
//       });
  
//       const result: BorrowBooksResponse = await response.json();
  
//       console.log("Server Response:", result); // Log server response for debugging
  
//       if (!response.ok) {
//         setStatusMessage(`Error: ${result.message || "Unknown error occurred."}`);
//         return;
//       }
  
//       setStatusMessage(`Success: Borrow order created with ID ${result.borrowId}`);
//     } catch (error) {
//       setStatusMessage((error as Error).message);
//     }
//   };
  

//   return (
//     <div style={{ padding: "20px", maxWidth: "500px", margin: "auto" }}>
//       <h1>Borrow Books</h1>

//       <div style={{ marginBottom: "15px" }}>
//         <label>
//           User ID:
//           <input
//             type="text"
//             value={userId}
//             onChange={(e) => setUserId(e.target.value)}
//             style={{ display: "block", width: "100%", padding: "8px", margin: "5px 0" }}
//           />
//         </label>
//       </div>

//       <div style={{ marginBottom: "15px" }}>
//         <label>
//           Borrow Date:
//           <input
//             type="date"
//             value={borrowDate}
//             onChange={(e) => setBorrowDate(e.target.value)}
//             style={{ display: "block", width: "100%", padding: "8px", margin: "5px 0" }}
//           />
//         </label>
//       </div>

//       <div style={{ marginBottom: "15px" }}>
//         <label>
//           Return Date:
//           <input
//             type="date"
//             value={returnDate}
//             onChange={(e) => setReturnDate(e.target.value)}
//             style={{ display: "block", width: "100%", padding: "8px", margin: "5px 0" }}
//           />
//         </label>
//       </div>

//       <div>
//         <h3>Book List</h3>
//         {bookList.map((book, index) => (
//           <div key={index} style={{ display: "flex", marginBottom: "10px" }}>
//             <input
//               type="text"
//               value={book.bookId}
//               onChange={(e) => handleBookChange(index, e.target.value)}
//               placeholder="Book ID"
//               style={{ flex: 1, padding: "8px", marginRight: "5px" }}
//             />
//             <button
//               type="button"
//               onClick={() => removeBookField(index)}
//               style={{
//                 backgroundColor: "#FF5555",
//                 color: "#FFF",
//                 border: "none",
//                 padding: "5px 10px",
//                 cursor: "pointer",
//               }}
//             >
//               Remove
//             </button>
//           </div>
//         ))}
//         <button
//           type="button"
//           onClick={addBookField}
//           style={{
//             padding: "10px",
//             backgroundColor: "#007BFF",
//             color: "#FFF",
//             border: "none",
//             cursor: "pointer",
//           }}
//         >
//           Add Book
//         </button>
//       </div>

//       <button
//         onClick={handleBorrowBooks}
//         style={{
//           marginTop: "20px",
//           padding: "10px 20px",
//           backgroundColor: "#007BFF",
//           color: "#FFF",
//           border: "none",
//           borderRadius: "5px",
//           cursor: "pointer",
//         }}
//       >
//         Submit Borrow Request
//       </button>

//       {statusMessage && (
//         <p
//           style={{
//             marginTop: "20px",
//             padding: "10px",
//             backgroundColor: statusMessage.startsWith("Error") ? "#FFDDDD" : "#DDFFDD",
//             color: statusMessage.startsWith("Error") ? "#FF0000" : "#008000",
//             border: `1px solid ${statusMessage.startsWith("Error") ? "#FF0000" : "#008000"}`,
//             borderRadius: "5px",
//           }}
//         >
//           {statusMessage}
//         </p>
//       )}
//     </div>
//   );
// };

// export default BorrowBooksPage;

