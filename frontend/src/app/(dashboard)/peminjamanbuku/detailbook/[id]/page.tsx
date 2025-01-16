"use client";

import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import Link from "next/link";
import Image from "next/image";
import { IoNotificationsCircle } from "react-icons/io5";
import LittleCard from "@/components/littlecard";

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

const BookDetailsPage: React.FC = () => {
  const router = useRouter();
  const [book, setBook] = useState<Book | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  // Use state to hold the ID from the query
  const [id, setId] = useState<string | null>(null);

  useEffect(() => {
    // Extract the ID from the pathname instead of relying on router.query
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
  }, [id]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;

  return (
    <div className="p-4 xl:p-6 mb-32">
        <div className="mb-5">
            <Link href={"/peminjamanbuku"} className="hover:underline font-sans">Back to Peminjaman Buku</Link>
        </div>
        {book && (
            <div className="flex justify-center items-center lg:justify-start mb-10">
                <span className="text-5xl text-gray-900 font-sans font-bold block">
                    {book.title}
                </span>
            </div>
        )}
        
        {book && (
            <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                <div className="flex justify-center w-full lg:w-1/2 lg:justify-start">
                    <Image 
                        src={"/buku1.png"}
                        width={400}
                        height={200}
                        alt="contoh buku"
                        className="flex items-center justify-center" />
                </div>
                <div className="mb-4 w-full max-h-full lg:ml-10 2xl:ml-0">
                    <div className="flex justify-center">
                        <p className="text-lg text-gray-700 font-sans text-justify underline mb-5">Sinopsis</p>
                    </div>
                    <p className="text-lg text-gray-500 font-sans text-justify ">
                        {book.desc}
                        Lorem ipsum dolor sit amet consectetur adipisicing elit. Necessitatibus alias odit in id velit hic tempora placeat architecto, earum fugiat vel quisquam est quos sed, sapiente laborum molestiae voluptate veritatis dignissimos, doloribus maxime. Necessitatibus voluptas expedita voluptatum pariatur iure numquam nobis quam laboriosam animi. Placeat, vero ullam autem minus dicta dolor repellat enim magnam sit atque, ex hic quam totam.
                    </p>
                    <p className="text-lg text-gray-700 font-sans text-justify ">
                        by: {book.authorName}
                    </p>
                    <IoNotificationsCircle className="w-10 h-10 mt-6 text-gray-600 hover:text-gray-700 cursor-pointer" />
                    <div className="mt-6 flex justify-start items-center lg:justify-start">
                        <button className="w-48 h-16 bg-gray-600 text-white hover:text-gray-600 hover:bg-white hover:border hover:border-gray-600">
                            Add to Cart
                        </button>
                        <p className="text-lg text-gray-600 ml-5 font-sans">Stok: {book.totalPage}</p>
                    </div>
                </div>
            </div>
        )};
        <div className="mt-20">
            <div className="w-full flex justify-center">
                <span className="font-sans font-bold text-4xl mb-6">Recommendation For You</span>
            </div>
            <div className="grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-4 w-full mt-10 xl:mt-0">
                <LittleCard />
                <LittleCard />
                <LittleCard />
                <LittleCard />
            </div>
        </div>
      {/* {book && (
        <>
          <h1 className="text-2xl font-bold">{book.title}</h1>
          <p>Author: {book.authorName}</p>
          <p>Publisher: {book.publisherName}</p>
          <p>Genre: {book.genre}</p>
          <p>Published: {book.datePublished}</p>
          <p>Pages: {book.totalPage}</p>
          <p>Country: {book.country}</p>
          <p>Language: {book.language}</p>
          <p>Description: {book.desc}</p>
        </>
      )} */}
    </div>
  );
};

export default BookDetailsPage;
