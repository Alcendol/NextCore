import Image from "next/image";

export default function Home() {
  return (
    <div className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]">
      <main className="flex flex-col gap-8 row-start-2 items-center sm:items-start">
        <Image
          className="dark:invert"
          src="/next.svg"
          alt="Next.js logo"
          width={180}
          height={38}
          priority
        />
        <ol className="list-inside list-decimal text-sm text-center sm:text-left font-[family-name:var(--font-geist-mono)]">
          <li className="mb-2">
            Get started by editing{" "}
            <code className="bg-black/[.05] dark:bg-white/[.06] px-1 py-0.5 rounded font-semibold">
              src/app/page.tsx
            </code>
            .
          </li>
          <li>Save and see your changes instantly.</li>
        </ol>

        <div className="flex gap-4 items-center flex-col sm:flex-row">
          <a
            className="rounded-full border border-solid border-transparent transition-colors flex items-center justify-center bg-foreground text-background gap-2 hover:bg-[#383838] dark:hover:bg-[#ccc] text-sm sm:text-base h-10 sm:h-12 px-4 sm:px-5"
            href="https://vercel.com/new?utm_source=create-next-app&utm_medium=appdir-template-tw&utm_campaign=create-next-app"
            target="_blank"
            rel="noopener noreferrer"
          >
            <Image
              className="dark:invert"
              src="/vercel.svg"
              alt="Vercel logomark"
              width={20}
              height={20}
            />
            Deploy now
          </a>
          <a
            className="rounded-full border border-solid border-black/[.08] dark:border-white/[.145] transition-colors flex items-center justify-center hover:bg-[#f2f2f2] dark:hover:bg-[#1a1a1a] hover:border-transparent text-sm sm:text-base h-10 sm:h-12 px-4 sm:px-5 sm:min-w-44"
            href="https://nextjs.org/docs?utm_source=create-next-app&utm_medium=appdir-template-tw&utm_campaign=create-next-app"
            target="_blank"
            rel="noopener noreferrer"
          >
            Read our docs
          </a>
        </div>
      </main>
      <footer className="row-start-3 flex gap-6 flex-wrap items-center justify-center">
        <a
          className="flex items-center gap-2 hover:underline hover:underline-offset-4"
          href="https://nextjs.org/learn?utm_source=create-next-app&utm_medium=appdir-template-tw&utm_campaign=create-next-app"
          target="_blank"
          rel="noopener noreferrer"
        >
          <Image
            aria-hidden
            src="/file.svg"
            alt="File icon"
            width={16}
            height={16}
          />
          Learn
        </a>
        <a
          className="flex items-center gap-2 hover:underline hover:underline-offset-4"
          href="https://vercel.com/templates?framework=next.js&utm_source=create-next-app&utm_medium=appdir-template-tw&utm_campaign=create-next-app"
          target="_blank"
          rel="noopener noreferrer"
        >
          <Image
            aria-hidden
            src="/window.svg"
            alt="Window icon"
            width={16}
            height={16}
          />
          Examples
        </a>
        <a
          className="flex items-center gap-2 hover:underline hover:underline-offset-4"
          href="https://nextjs.org?utm_source=create-next-app&utm_medium=appdir-template-tw&utm_campaign=create-next-app"
          target="_blank"
          rel="noopener noreferrer"
        >
          <Image
            aria-hidden
            src="/globe.svg"
            alt="Globe icon"
            width={16}
            height={16}
          />
          Go to nextjs.org →
        </a>
      </footer>
    </div>
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
