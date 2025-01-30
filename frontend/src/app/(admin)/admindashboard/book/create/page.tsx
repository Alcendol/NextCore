"use client";

import Link from "next/link"
import Image from "next/image";
import { useEffect, useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";
import { useRouter } from "next/navigation";


interface Author {
    authorId: string;
    firstName: string;
    lastName: string;
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
    const [authors, setAuthors] = useState<Author[]>([]);
    const [selectedAuthor, setSelectedAuthor] = useState<string[]>([]); // Change to array of strings
    const [publishers, setPublishers] = useState<Publisher[]>([]);
    const [selectedPublisher, setSelectedPublisher] = useState<string[]>([]); // Change to array of strings
    const [genres, setGenres] = useState<Genre[]>([]);
    const [selectedGenre, setSelectedGenre] = useState<string[]>([]);
    const [preview, setPreview] = useState<string | null>(null);
    const [imageBase64, setImageBase64] = useState<string | null>(null)
    const [imageBook, setImageBook] = useState<File | null>(null);
    
    const [isAuthorDropdownOpen, setIsAuthorDropdownOpen] = useState(false);
    const [isPublisherDropdownOpen, setIsPublisherDropdownOpen] = useState(false);
    const [isGenreDropdownOpen, setIsGenreDropdownOpen] = useState(false);

    const [bookId, setBookId] = useState("");
    const [title, setTitle] = useState("");
    const [datePublished, setDatePublished] = useState("");
    const [totalPage, setTotalPage] = useState("");
    const [country, setCountry] = useState("");
    const [language, setLanguage] = useState("");
    const [desc, setDesc] = useState("");
    const [image, setImage] = useState<File | null>(null);
    const [mediaType, setMediaType] = useState("[0,0,1]");
    const [stock, setStock] = useState("");
    
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

    const handleAuthorSelect = (author: string) => {
        if (!selectedAuthor.includes(author)) {
            setSelectedAuthor([...selectedAuthor, author]);
        }
        setIsAuthorDropdownOpen(false);
    };

    const handlePublisherSelect = (publisher: string) => {
        if (!selectedPublisher.includes(publisher)) {
            setSelectedPublisher([...selectedPublisher, publisher]);
        }
        setIsPublisherDropdownOpen(false); 
    };

    const handleGenreSelect = (genre: string) => {
        if (!selectedGenre.includes(genre)) {
            setSelectedGenre([...selectedGenre, genre]);
        }
        setIsGenreDropdownOpen(false); 
    };

    const handleTagRemove = (type: string, value: string) => {
        if (type === "author") {
            setSelectedAuthor(selectedAuthor.filter((item) => item !== value));
        } else if (type === "publisher") {
            setSelectedPublisher(selectedPublisher.filter((item) => item !== value));
        } else if (type === "genre") {
            setSelectedGenre(selectedGenre.filter((item) => item !== value));
        }
    };

    const toggleAuthorDropdown = () => {
        setIsAuthorDropdownOpen((prev) => !prev);
    };

    const togglePublisherDropdown = () => {
        setIsPublisherDropdownOpen((prev) => !prev);
    };

    const toggleGenreDropdown = () => {
        setIsGenreDropdownOpen((prev) => !prev);
    };

    const router = useRouter();
    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
    
        if (!bookId || !title || !selectedAuthor.length || !selectedPublisher.length || !datePublished || !totalPage || !country || !language || !selectedGenre.length || !desc || !image || !stock) {
            alert("All fields are required.");
            return;
        }
    
        try {
            const formData = new FormData();
            formData.append("bookId", bookId);
            formData.append("title", title);
    
            selectedAuthor.forEach((author) => formData.append("authorNames", author));
            selectedPublisher.forEach((publisher) => formData.append("publisherNames", publisher));
            selectedGenre.forEach((genre) => formData.append("genres", genre));
    
            formData.append("datePublished", datePublished);
            formData.append("totalPage", totalPage);
            formData.append("country", country);
            formData.append("language", language);
            formData.append("description", desc);
            formData.append("image", image);
            formData.append("stock", stock);
    
            formData.append("mediaType", mediaType);
    
            for (let pair of formData.entries()) {
                console.log(`${pair[0]}:`, pair[1]);
            }
    
            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/book/single`, {
                method: "POST",
                body: formData,
            });
    
            if (res.ok) {
                router.push("/admindashboard/book");
            } else {
                console.log(formData);
                const errorData = await res.json();
                console.error("Error response:", errorData);
                alert(`Error: ${errorData?.title || "Failed to Create New Book"}`);
            }
        } catch (error) {
            console.error("Error during submission:", error);
        }
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
                <form className="bg-white border border-gray-400 p-4 w-full" onSubmit={handleSubmit}>
                    <label htmlFor="bookId" className="block text-sm text-gray-700 font-sans mb-2">
                        ISBN:
                    </label>
                    <input
                        id="bookId"
                        type="text"
                        placeholder="e.g: 123-1234-..."
                        onChange={(e) => setBookId(e.target.value)}
                        value={bookId}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="title" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Title:
                    </label>
                    <input
                        id="title"
                        type="text"
                        onChange={(e) => setTitle(e.target.value)}
                        placeholder="e.g: Buku Pelajaran Bahasa Indonesia."
                        value={title}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <div className="relative mt-4">
                        <label htmlFor="authors" className="block mt-4 font-sans text-sm text-gray-700">Authors:</label>
                        <div className="flex flex-wrap gap-2 mt-2">
                            {selectedAuthor.map((author, index) => (
                                <div key={index} className="flex items-center space-x-2 bg-blue-100 rounded px-2 py-1">
                                    <span>{author}</span>
                                    <button
                                        type="button"
                                        onClick={() => handleTagRemove("author", author)}
                                        className="text-red-500"
                                    >
                                        &times;
                                    </button>
                                </div>
                            ))}
                        </div>
                        <button type="button" onClick={toggleAuthorDropdown} className="w-full p-2 border rounded bg-gray-200 mt-2 text-left">
                            Select Authors
                        </button>
                        {isAuthorDropdownOpen && (
                            <div className="absolute w-full bg-white border mt-1 rounded shadow-lg z-10">
                                <div className="max-h-60 overflow-y-auto">
                                    {authors.map((author) => (
                                        <button
                                            key={author.authorId}
                                            type="button"
                                            onClick={() => handleAuthorSelect(author.firstName)}
                                            className="w-full text-left p-2 hover:bg-gray-200"
                                        >
                                            {author.firstName} {author.lastName}
                                        </button>
                                    ))}
                                </div>
                            </div>
                        )}
                    </div>
                    <label htmlFor="datePublished" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Published Date:
                    </label>
                    <input
                        id="datePublished"
                        type="date"
                        onChange={(e) => setDatePublished(e.target.value)}
                        value={datePublished}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400 text-gray-400"
                    />
                    <div className="relative mt-4">
                        <label htmlFor="publishers" className="block mt-4 font-sans text-sm text-gray-700">Publishers:</label>
                        <div className="flex flex-wrap gap-2 mt-2">
                            {selectedPublisher.map((publisher, index) => (
                                <div key={index} className="flex items-center space-x-2 bg-green-100 rounded px-2 py-1">
                                    <span>{publisher}</span>
                                    <button
                                        type="button"
                                        onClick={() => handleTagRemove("publisher", publisher)}
                                        className="text-red-500"
                                    >
                                        &times;
                                    </button>
                                </div>
                            ))}
                        </div>
                        <button type="button" onClick={togglePublisherDropdown} className="w-full p-2 border rounded bg-gray-200 mt-2 text-left">
                            Select Publishers
                        </button>
                        {isPublisherDropdownOpen && (
                            <div className="absolute w-full bg-white border mt-1 rounded shadow-lg z-10">
                                <div className="max-h-60 overflow-y-auto">
                                    {publishers.map((publisher) => (
                                        <button
                                            key={publisher.publisherId}
                                            type="button"
                                            onClick={() => handlePublisherSelect(publisher.publisherName)}
                                            className="w-full text-left p-2 hover:bg-gray-200"
                                        >
                                            {publisher.publisherName}
                                        </button>
                                    ))}
                                </div>
                            </div>
                        )}
                    </div>
                    <label htmlFor="totalPage" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Total Pages:
                    </label>
                    <input
                        id="totalPage"
                        type="number"
                        onChange={(e) => setTotalPage(e.target.value)}
                        value={totalPage}
                        placeholder="e.g: 128"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <div className="relative mt-4">
                        <label htmlFor="genres" className="block mt-4 font-sans text-sm text-gray-700">Genres:</label>
                        <div className="flex flex-wrap gap-2 mt-2">
                            {selectedGenre.map((genre, index) => (
                                <div key={index} className="flex items-center space-x-2 bg-yellow-100 rounded px-2 py-1">
                                    <span>{genre}</span>
                                    <button
                                        type="button"
                                        onClick={() => handleTagRemove("genre", genre)}
                                        className="text-red-500"
                                    >
                                        &times;
                                    </button>
                                </div>
                            ))}
                        </div>
                        <button type="button" onClick={toggleGenreDropdown} className="w-full p-2 border rounded bg-gray-200 mt-2 text-left">
                            Select Genres
                        </button>
                        {isGenreDropdownOpen && (
                            <div className="absolute w-full bg-white border mt-1 rounded shadow-lg z-10">
                                <div className="max-h-60 overflow-y-auto">
                                    {genres.map((genre) => (
                                        <button
                                            key={genre.genreId}
                                            type="button"
                                            onClick={() => handleGenreSelect(genre.genreName)}
                                            className="w-full text-left p-2 hover:bg-gray-200"
                                        >
                                            {genre.genreName}
                                        </button>
                                    ))}
                                </div>
                            </div>
                        )}
                    </div>
                    <label htmlFor="desc" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Description:
                    </label>
                    <input
                        id="desc"
                        type="text"
                        onChange={(e) => setDesc(e.target.value)}
                        value={desc}
                        placeholder="e.g: Pelajaran Bahasa Indonesia adalah ...."
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="country" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Country:
                    </label>
                    <input
                        id="country"
                        type="text"
                        onChange={(e) => setCountry(e.target.value)}
                        value={country}
                        placeholder="e.g: Indonesia, Amerika, Prancis, ..."
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="language" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Bahasa:
                    </label>
                    <input
                        id="language"
                        type="text"
                        onChange={(e) => setLanguage(e.target.value)}
                        value={language}
                        placeholder="e.g: Bahasa Indonesia, Bahasa Inggris, Bahas China, ...."
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="image" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Upload Image:
                    </label>
                    <input
                        id="image"
                        name="image"
                        type="file"
                        accept="image/*"
                        required
                        onChange={(e) => setImage(e.target.files?.[0] || null)}
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />

                    {preview && (
                        <div className="mt-4">
                            <Image
                                src={preview}
                                alt="Book Preview"
                                width={200}
                                height={300}
                                className="rounded-lg border"
                            />
                        </div>
                    )}

                    <label htmlFor="stock" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Stock:
                    </label>
                    <input
                        id="stock"
                        type="number"
                        onChange={(e) => setStock(e.target.value)}
                        value={stock}
                        placeholder="e.g: 10"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <div className="flex justify-end">
                        <Link href="/admindashboard/book">
                            <button
                                className="mt-6 w-32 mx-2 bg-gray-100 border-2 text-white p-2 rounded-lg hover:bg-gray-200 disabled:bg-gray-300"
                            >
                                <span className="font-sans font-bold text-gray-600">
                                    Cancel
                                </span>
                            </button>
                        </Link>
                        <button
                            className="mt-6 w-32 bg-blue-500 text-white p-2 rounded-lg hover:bg-blue-600 disabled:bg-gray-300"
                        >
                            <span className="font-sans font-bold text-white">
                                Tambah
                            </span>
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default CreatePageBook;
