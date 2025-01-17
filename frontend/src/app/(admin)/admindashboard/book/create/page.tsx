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
interface Author {
    authorId: string;
    authorName: string;
    authorEmail: string;
    authorPhone: string;
}

interface Publisher {
    publisherId: string;
    publisherName: string;
    publisherEmail: string;
    publisherPhone: string;
}

interface Genre {
    genreId: string;
    genreName: string;
}

const CreatePageBook = () => {
    const[authors, setAuthors] = useState<Author[]>([]);
    const [selectedAuthor, setSelectedAuthor] = useState<string | null>(null);
    const[publishers, setPublishers] = useState<Publisher[]>([]);
    const [selectedPublisher, setSelectedPublisher] = useState<string | null>(null);
    const[genres, setGenres] = useState<Genre[]>([]);
    const [selectedGenre, setSelectedGenre] = useState<string | null>(null);
    
    useEffect(() => {
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/author`)
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Failed to fetch authors.");
                }
                return response.json();
            })
            .then((data) => {
                setAuthors(data);
            })
            .catch((error) => console.error("Error fetching authors:", error));
    }, []);

    const handleAuthorChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedAuthor(e.target.value);
    };

    useEffect(() => {
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/publisher`)
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Failed to fetch publisher.");
                }
                return response.json();
            })
            .then((data) => {
                setPublishers(data);
            })
            .catch((error) => console.error("Error fetching publisher:", error));
    }, []);

    const handlePublisherChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedPublisher(e.target.value);
    };

    useEffect(() => {
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/genre`)
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Failed to fetch genres.");
                }
                return response.json();
            })
            .then((data) => {
                setGenres(data);
            })
            .catch((error) => console.error("Error fetching genres:", error));
    }, []);

    const handleGenreChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedGenre(e.target.value);
    };

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            <div className="mb-5">
                <Link href={"/admindashboard/book"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Peminjaman Buku
                </Link>
            </div>
            <div className="w-full flex justify-start items-center mb-5">
                <span className="font-sans font-bold text-xl">Tambah Buku</span>
            </div>
            <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                <div className="bg-white border border-gray-400 p-4 w-full">
                    <label htmlFor="ISBN" className="block text-sm text-gray-700 font-sans mb-2">
                        ISBN:
                    </label>
                    <input
                        id="bookId"
                        type="text"
                        placeholder="e.g: 123-1234-..."
                        // onChange={handleInputChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="title" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Title:
                    </label>
                    <input
                        id="title"
                        type="text"
                        placeholder="e.g: Buku Pelajaran Bahasa Indonesia."
                        // onChange={handleInputChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="authorName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Author:
                    </label>
                    <select
                        id="authorName"
                        value={selectedAuthor || ""}
                        onChange={handleAuthorChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    >
                        <option value="" disabled>
                            Select an author
                        </option>
                        {authors.map((author) => (
                            <option key={author.authorId} value={author.authorName}>
                                {author.authorName}
                            </option>
                        ))}
                    </select>
                    <label htmlFor="datePublished" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Published Date:
                    </label>
                    <input
                        id="datePublished"
                        type="date"
                        // onChange={handleInputChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400 text-gray-400"
                    />
                    <label htmlFor="publisherName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Publisher:
                    </label>
                    <select
                        id="publisherName"
                        value={selectedPublisher || ""}
                        onChange={handlePublisherChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    >
                        <option value="" disabled>
                            Select an Publisher
                        </option>
                        {publishers.map((publisher) => (
                            <option key={publisher.publisherId} value={publisher.publisherName}>
                                {publisher.publisherName}
                            </option>
                        ))}
                    </select>
                    <label htmlFor="totalPage" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Total Pages:
                    </label>
                    <input
                        id="totalPage"
                        type="number"
                        placeholder="e.g: 128"
                        // onChange={handleInputChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="genreName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Genre:
                    </label>
                    <select
                        id="genreName"
                        value={selectedGenre || ""}
                        onChange={handleGenreChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    >
                        <option value="" disabled>
                            Select an Genre
                        </option>
                        {genres.map((genre) => (
                            <option key={genre.genreId} value={genre.genreName}>
                                {genre.genreName}
                            </option>
                        ))}
                    </select>
                    <label htmlFor="desc" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Description:
                    </label>
                    <input
                        id="desc"
                        type="text"
                        placeholder="e.g: Pelajaran Bahasa Indonesia adalah ...."
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
                            className="mt-6 w-32 bg-blue-500 text-white p-2 rounded-lg hover:bg-blue-600 disabled:bg-gray-300"
                        >
                            <span className="font-sans font-bold text-white">
                                Tambah
                            </span>
                            {/* {isUpdating ? "Updating..." : "Update Book"} */}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default CreatePageBook;