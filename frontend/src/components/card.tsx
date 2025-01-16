import { useRouter } from "next/navigation";
import Image from "next/image";
import React from "react";
import Link from "next/link";
import { pages } from "next/dist/build/templates/app-page";

interface CardProps {
  bookId: string;
  title: string;
  authorName: string;
  desc: string;
  genre: string[];
  datePublished: string;
}

const Card: React.FC<CardProps> = ({ bookId, title, desc, genre, datePublished, authorName }) => {
  const router = useRouter();

  const handleReadMore = () => {
    router.push(`/peminjamanbuku/detailbook/${bookId}`);
  };
  
  const addToCart = () => {

  };

  return (
    <section className="dark:bg-gray-900 rounded-lg">
        <div className="py-4 mx-auto w-full lg:py-8 lg:px-6">
            <div className="grid gap-8">
                <article key={title}
                    className="p-6 bg-white rounded-lg border border-gray-200 shadow-md dark:bg-gray-800 dark:border-gray-700">
                    <div className="flex flex-col md:flex-row gap-4">
                        <div className="flex-shrink-0">
                            <Image
                            src="/buku1.png"
                            alt="Book Cover"
                            width={200}
                            height={200}
                            className="rounded-lg"
                            />
                        </div>
                        <div className="flex flex-col flex-1 justify-between">
                            <div className="flex justify-between">
                                <h2 className="mb-2 text-lg md:text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                                    <span className="hover:underline">{title}</span>
                                    <span> - </span>
                                    <span className="hover:underline font-light text-gray-500 text-lg md:text-xl">
                                    {authorName}
                                    </span>
                                </h2>
                                <span className="text-sm text-gray-500 dark:text-gray-400">{datePublished}</span>
                            </div>
                            <p className="h-32">{desc}</p>
                            <div className="flex flex-wrap gap-2 mt-2">
                                {(genre || []).map((g, index) => (
                                    <Link key={index} href={`/genre/${g.trim()}`}>
                                    <span className="bg-blue-500 text-white text-xs font-medium inline-flex items-center px-2.5 py-1.5 rounded hover:underline">
                                        {g.trim()}
                                    </span>
                                    </Link>
                                ))}
                            </div>
                            <div className="mr-5 w-full">
                                <div className="flex mt-4 w-4/6">
                                    <button
                                        onClick={handleReadMore}
                                        className="rounded-lg bg-gray-600 hover:bg-gray-700 text-white w-full h-14 mr-5"
                                    >
                                        Read More
                                    </button>
                                    <button
                                        onClick={addToCart}
                                        className="rounded-lg bg-yellow-500 hover:bg-yellow-600 text-white w-full h-14"
                                    >
                                        Add to Cart
                                    </button>
                                    <p className="flex justify-center items-center ml-5">Stok: </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </article>
            </div>
        </div>
    </section>
  );
};

export default Card;
