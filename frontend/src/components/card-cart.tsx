"use client";

import { IoNotificationsCircleSharp } from "react-icons/io5";
import Image from "next/image";
import React, { useState } from "react";

const CardCart = () => {
    const [selectedItems, setSelectedItems] = useState([]);

    const handleCheckboxChange = (id) => {
        setSelectedItems((prevSelected) =>
            prevSelected.includes(id)
                ? prevSelected.filter((item) => item !== id)
                : [...prevSelected, id]
        );
    };

    const books = [
        { id: 1, title: "Book 1", author: "Author 1", image: "/buku1.png" },
        { id: 2, title: "Book 2", author: "Author 2", image: "/buku1.png" },
        { id: 3, title: "Book 3", author: "Author 3", image: "/buku1.png" },
    ];

    return (
        <div className="relative">
            <section className="dark:bg-gray-900 rounded-lg mx-5">
                <div className="py-4 mx-auto w-full lg:py-8">
                    <div className="grid gap-8">
                        {books.map((book) => (
                            <article
                                key={book.id}
                                className={`p-6 rounded-lg border border-gray-200 shadow-md ${
                                    selectedItems.includes(book.id)
                                        ? "bg-blue-200 bg-opacity-50"
                                        : "bg-white dark:bg-gray-800 dark:border-gray-700"}`}>
                                <div className="flex items-start gap-4">
                                    <div className="flex-shrink-0">
                                        <input
                                            type="checkbox"
                                            checked={selectedItems.includes(book.id)}
                                            onChange={() => handleCheckboxChange(book.id)}
                                            className="w-5 h-5 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                                        />
                                    </div>

                                    <div className="flex flex-col md:flex-row gap-4 flex-1">
                                        <div className="flex-shrink-0">
                                            <Image
                                                src={book.image}
                                                alt="Book Cover"
                                                width={200}
                                                height={200}
                                                className="rounded-lg"
                                            />
                                        </div>

                                        <div className="flex flex-col flex-1 justify-between">
                                            <div className="flex justify-between">
                                                <h2 className="mb-2 text-lg md:text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                                                    <a href={`/home`} className="hover:underline">
                                                        {book.title}
                                                    </a>
                                                    <span> - </span>
                                                    <a
                                                        href={`/home`}
                                                        className="hover:underline font-light text-gray-500 text-lg md:text-xl"
                                                    >
                                                        {book.author}
                                                    </a>
                                                </h2>
                                                <span className="text-sm text-gray-500 dark:text-gray-400">
                                                    {"Tanggal Rilis"}
                                                </span>
                                            </div>
                                            <div>
                                                <span className="bg-blue-500 text-white text-xs font-medium inline-flex items-center px-2.5 py-1.5 mt-2 rounded mr-3">
                                                    <a href={`/home`} className="hover:underline"></a>
                                                </span>
                                            </div>
                                            <div className="mt-4">
                                                <IoNotificationsCircleSharp className="w-10 h-10 text-yellow-500" />
                                            </div>

                                            <div className="flex mt-4">
                                                <a href={`/home`} className="text-sm font-medium hover:underline mr-5">
                                                    <button className="rounded-sm bg-blue-500 text-white w-full px-5 h-10">
                                                        Read More
                                                    </button>
                                                </a>

                                                <a href={`/home`} className="text-sm font-medium hover:underline">
                                                    <button className="rounded-sm bg-yellow-500 text-black w-full px-5 h-10">
                                                        Add to Cart{" "}
                                                        <span className="w-2 h-2 rounded-full border border-black px-1">
                                                            +
                                                        </span>
                                                    </button>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        ))}
                    </div>
                </div>
            </section>

            {selectedItems.length > 0 && (
                <div className="fixed bottom-0 left-0 w-full bg-gray-800 text-white py-4 px-6">
                    <div className="flex justify-between items-center px-52">
                        <span>
                            You have selected {selectedItems.length}{" "}
                            {selectedItems.length === 1 ? "item" : "items"}.
                        </span>
                        <button
                            onClick={() => alert(`Proceeding to checkout with ${selectedItems.length} items!`)}
                            className="bg-white text-blue-600 font-bold px-5 py-2 rounded-lg hover:bg-gray-200"
                        >
                            Checkout
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
};

export default CardCart;
