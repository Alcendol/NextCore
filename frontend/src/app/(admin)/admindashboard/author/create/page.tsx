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

const CreatePageAuthor = () => {
    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            <div className="mb-5">
                <Link href={"/admindashboard/author"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Author
                </Link>
            </div>
            <div className="w-full flex justify-start items-center mb-5">
                <span className="font-sans font-bold text-xl">Tambah Author</span>
            </div>
            <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                <div className="bg-white border border-gray-400 p-4 w-full">
                    <label htmlFor="authorName" className="block text-sm text-gray-700 font-sans mb-2">
                        Nama Author:
                    </label>
                    <input
                        id="authorName"
                        type="text"
                        placeholder="e.g: Tere Liye"
                        // onChange={handleInputChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="authorEmail" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Email Author:
                    </label>
                    <input
                        id="authorEmail"
                        type="text"
                        placeholder="e.g: xxxx@xxx.xx"
                        // onChange={handleInputChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="authorPhone" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Nomor Hp Author:
                    </label>
                    <input
                        id="authorPhone"
                        type="text"
                        placeholder="e.g: 08..."
                        // onChange={handleInputChange}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <div className="flex justify-end">
                        <Link href="/admindashboard/author">
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

export default CreatePageAuthor;