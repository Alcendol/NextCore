import { IoSearch } from "react-icons/io5"
import { IoNotificationsCircleSharp } from "react-icons/io5"
import Image from "next/image"
import React from "react";

interface CardProps {
    title: string;
    authorName: string;
    desc: string;
    genre: string;
    datePublished: string;
  }

const Card: React.FC<CardProps> = ({ title, desc, genre, datePublished, authorName }) => {
    return (
        <section className="dark:bg-gray-900 rounded-lg">
            <div className="py-4 mx-auto w-full lg:py-8 lg:px-6">
                <div className="grid gap-8 ">
                <article
                    key={title}
                    className="p-6 bg-white rounded-lg border border-gray-200 shadow-md dark:bg-gray-800 dark:border-gray-700"
                    >
                    <div className="flex flex-col md:flex-row gap-4">
                        {/* Image Section */}
                        <div className="flex-shrink-0">
                        <Image src="/buku1.png" alt="Book Cover"
                            width={200}
                            height={200}
                            className="rounded-lg"
                        />
                        </div>

                        <div className="flex flex-col flex-1 justify-between">
                            <div className="flex justify-between">
                                <h2 className="mb-2 text-lg md:text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                                    <a href={`/home`} className="hover:underline">
                                        {title}
                                    </a>
                                    <span> - </span>
                                    <a href={`/home`} className="hover:underline font-light text-gray-500 text-lg md:text-xl">
                                        {authorName}
                                    </a>
                                </h2>
                                <span className="text-sm text-gray-500 dark:text-gray-400">
                                    {datePublished}
                                </span>
                            </div>
                            <div className="">
                                <p>{desc}</p>
                                <span className="bg-blue-500 text-white text-xs font-medium inline-flex items-center px-2.5 py-1.5 mt-2 rounded mr-3">
                                    <a href={`/home`} className="hover:underline">
                                        {genre}
                                    </a>
                                </span>
                            </div>
                            <div className="mt-4">
                                <IoNotificationsCircleSharp className="w-10 h-10 text-yellow-500"/>
                            </div>

                            {/* Author */}
                            <div className="flex mt-4">
                                <a href={`/home`} className="text-sm font-medium hover:underline mr-5">
                                    <button className="rounded-sm bg-blue-500 text-white w-full px-5 h-10">Read More</button>
                                </a>

                                <a href={`/home`} className="text-sm font-medium hover:underline">
                                    <button className="rounded-sm bg-yellow-500 text-black w-full px-5 h-10">Add to Cart <span className="w-2 h-2 rounded-full border border-black px-1">+</span></button>
                                </a>
                            </div>
                        </div>
                    </div>
                    </article>
                </div>
            </div>
        </section>
    )
}

export default Card