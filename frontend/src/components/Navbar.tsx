import Image from "next/image"
import Link from "next/link"
import { useState } from "react";

const Navbar = () => {
    const [isMenuOpen, setIsMenuOpen] = useState(false);

    // Toggle the menu
    const toggleMenu = () => {
      setIsMenuOpen((prev) => !prev);
    };
    return (
        <header className="fixed top-0 left-0 w-full h-20 bg-gray-100 border-b border-gray-200 z-50">
            <div className="container mx-auto flex justify-between items-center h-full px-4 sm:px-6 lg:px-8">
                <div className="flex items-center gap-2">
                    <Link href="/home" className="flex items-center gap-2">
                    <Image
                        src="/logo.jpeg"
                        alt="logo"
                        width={30}
                        height={30}
                        className="rounded-full"
                    />
                    <span className="text-xl font-bold text-gray-700">TamanBaca</span>
                    </Link>
                </div>

                {/* Desktop Navigation */}
                <nav className="hidden xl:flex items-center gap-8">
                    <ul className="flex items-center gap-6 text-gray-700 text-sm font-medium">
                        <div className="h-20 w-full hover:bg-slate-300 flex items-center px-3">
                            <li className="hover:bg-slate-300 mx-auto">
                                <Link href="/home">Home</Link>
                            </li>
                        </div>
                        <div className="h-20 w-full hover:bg-slate-300 flex items-center px-3">
                            <li className="hover:bg-slate-300 mx-auto">
                                <Link href="/aboutus" >About Us</Link>
                            </li>
                        </div>
                        <div className="h-20 w-full hover:bg-slate-300 flex items-center px-3">
                            <li className="hover:bg-slate-300 mx-auto">
                                <Link href="/peminjamanbuku" >Peminjaman Buku</Link>
                            </li>
                        </div>
                        <div className="h-20 w-full hover:bg-slate-300 flex items-center px-3">
                            <li className="hover:bg-slate-300 mx-auto">
                                <Link href="/ebook">E-Book</Link>
                            </li>
                        </div>
                        <div className="h-20 w-full hover:bg-slate-300 flex items-center px-3">
                            <li className="hover:bg-slate-300 mx-auto">
                                <Link href="/audiobook">AudioBook</Link>
                            </li>
                        </div>
                    </ul>

                    <div className="flex items-center gap-4">
                        <div className="flex items-center justify-between p-4">
                            <div className="hidden xl:flex items-center gap-2 text-xs rounded-full ring-[1.5px] ring-gray-200 px-3">
                                <Image src="/search.png" alt="" width={14} height={14}></Image>
                                <input type="text" placeholder="Search...." className="w-[200px] p-2 bg-transparent outline-none" />
                            </div>
                            <div className="hidden"></div>
                        </div>
                        <Link
                            href="/signin"
                            className="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-200 transition"
                        >
                            Login
                        </Link>
                        <Link
                            href="/signup"
                            className="px-4 py-2 bg-blue-500 text-white rounded-md text-sm font-medium hover:bg-blue-600 transition"
                        >
                            Signup
                        </Link>
                    </div>
                </nav>

                <button
                    className="xl:hidden flex items-center justify-center w-8 h-8 text-gray-700 border border-gray-300 rounded-md"
                    onClick={toggleMenu}
                    aria-label="Toggle navigation menu"
                >
                    {isMenuOpen ? "✖" : "☰"}
                </button>
            </div>

            {/* Mobile Menu */}
            {isMenuOpen && (
                <div className="xl:hidden bg-gray-800 border-t border-gray-200 py-10 px-5 sm:px-5">
                    <ul className="flex flex-col items-start gap-4 text-white text-sm font-medium md:px-32">
                        <Link href="/home" className="w-full" onClick={toggleMenu}>
                            <li className="w-full h-12 hover:bg-gray-600 flex items-center">
                                Home
                            </li>
                        </Link>
                        <Link href="/aboutus" className="w-full" onClick={toggleMenu}>
                            <li className="w-full h-12 hover:bg-gray-600 flex items-center">
                                About Us
                            </li>
                        </Link>
                        <Link href="/peminjamanbuku" className="w-full" onClick={toggleMenu}>
                            <li className="w-full h-12 hover:bg-gray-600 flex items-center">
                                Peminjaman Buku
                            </li>
                        </Link>
                        <Link href="/ebook" className="w-full" onClick={toggleMenu}>
                            <li className="w-full h-12 hover:bg-gray-600 flex items-center">
                                E-Book
                            </li>
                        </Link>
                        <Link href="/audiobook" className="w-full" onClick={toggleMenu}>
                            <li className="w-full h-12 hover:bg-gray-600 flex items-center">
                                Audio Book
                            </li>
                        </Link>
                        <li className="flex justify-between w-full">
                            <Link
                                href="/signin"
                                className="px-4 py-2 border bg-slate-200 border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-400 transition w-5/6 mr-5 flex justify-center"
                                onClick={toggleMenu}>
                                    Login
                            </Link>
                            <Link
                                href="/signup"
                                className="px-4 py-2 bg-blue-500 text-white rounded-md text-sm font-medium hover:bg-blue-600 transition w-5/6 ml-5 flex justify-center"
                                onClick={toggleMenu}
                                >
                                    Signup
                            </Link>
                        </li>
                        <li className="w-full">
                            <div className="flex items-center justify-between py-4 w-full">
                                <div className="flex items-center gap-2 text-md rounded-lg ring-[1.5px] ring-gray-200 px-3 w-full">
                                    <Image src="/search.png" alt="" width={15} height={15}></Image>
                                    <input type="text" placeholder="Search...." className="w-full p-2 bg-transparent outline-none" />
                                </div>
                                <div className="hidden"></div>
                            </div>
                        </li>
                    </ul>
                </div>
            )}
        </header>
    )
}

export default Navbar