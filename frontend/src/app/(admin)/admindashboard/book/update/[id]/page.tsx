"use client";

import Link from "next/link"
import Image from "next/image";
import { useEffect, useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";


interface Book {
    bookId: string;
    title: string;
    authorName: string;
    publisherName: string;
    datePublished: string;
    totalPage: number;
    country: string;
    language: string;
    genre: string;
    desc: string;
}

const UpdatePageBook: React.FC = () => {
    const [book, setBook] = useState<Book | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [id, setId] = useState<string | null>(null);

    useEffect(() => {
        const pathname = window.location.pathname;
        const idFromPath = pathname.split("/").pop();
        setId(idFromPath || null);
        }, []);
    
        useEffect(() => {
            if (id) {
            fetch(`${process.env.NEXT_PUBLIC_API_URL}/book/by-id/${id}`)
                .then((response) => {
                if (!response.ok) {
                    throw new Error("Failed to fetch book details.");
                }
                return response.json();
                })
                .then((data) => {
                setBook(data);
                setLoading(false);
                })
                .catch((error) => {
                console.error(error);
                setError("Failed to load book details.");
                setLoading(false);
                });
            }
        }, 
    [id]);
    if (loading) return <p>Loading...</p>;
    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            <div className="mb-5">
                <Link href={"/admindashboard/book"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Peminjaman Buku
                </Link>
            </div>
            {book && (
                <div className="w-full flex justify-start items-center mb-5">
                    <span className="font-sans font-bold text-xl">Update Data Buku {book.title}</span>
                </div>
            )}
            {book && (
                <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                    <div className="bg-white border border-gray-400 p-4 w-full">
                        <label htmlFor="ISBN" className="block text-sm text-gray-700 font-sans mb-2">
                            ISBN:
                        </label>
                        <input
                            id="bookId"
                            type="text"
                            defaultValue={book.bookId}
                            // onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="title" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Title:
                        </label>
                        <input
                            id="title"
                            type="text"
                            defaultValue={book.title}
                            // onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="authorName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Author:
                        </label>
                        <input
                            id="authorName"
                            type="text"
                            defaultValue={book.authorName}
                            // onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="datePublished" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Published Date:
                        </label>
                        <input
                            id="datePublished"
                            type="text"
                            defaultValue={book.datePublished}
                            // onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="publisherName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Publisher Name:
                        </label>
                        <input
                            id="publisherName"
                            type="text"
                            defaultValue={book.publisherName}
                            // onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="totalPage" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Total Pages:
                        </label>
                        <input
                            id="totalPage"
                            type="number"
                            defaultValue={book.totalPage}
                            // onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="genre" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Genre:
                        </label>
                        <input
                            id="genre"
                            type="text"
                            defaultValue={book.genre}
                            // onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="desc" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Description:
                        </label>
                        <input
                            id="desc"
                            type="text"
                            defaultValue={book.desc}
                            // onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <div className="flex justify-end">
                            <Link href="/admindashboard/book">
                                <button
                                    // onClick={handleUpdate}
                                    // disabled={isUpdating}
                                    className="mt-6 w-32 mx-2 bg-gray-100 border-2 text-white p-2 rounded-lg hover:bg-gray-200 disabled:bg-gray-300"
                                >
                                    <span className="font-sans font-bold text-gray-600">
                                        Cancel
                                    </span>
                                    {/* {isUpdating ? "Updating..." : "Update Book"} */}
                                </button>
                            </Link>
                            <button
                                // onClick={handleUpdate}
                                // disabled={isUpdating}
                                className="mt-6 w-32 bg-yellow-400 border-2 border-yellow-300 text-white p-2 rounded-lg hover:bg-yellow-500 disabled:bg-gray-300"
                            >
                                <span className="font-sans font-bold text-gray-600">
                                    Update
                                </span>
                                {/* {isUpdating ? "Updating..." : "Update Book"} */}
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default UpdatePageBook;