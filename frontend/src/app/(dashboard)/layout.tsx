"use client";

import { useState } from "react";
import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import Link from "next/link";
import Image from "next/image";
import Navbar from "@/components/Navbar";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});


export default function DashboardLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  // Toggle the menu
  const toggleMenu = () => {
    setIsMenuOpen((prev) => !prev);
  };
  return (
    <>
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
              <Navbar />
              <Link
                href="/login"
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
          <div className="xl:hidden bg-black border-t border-gray-200">
            <ul className="flex flex-col items-start gap-4 py-4 text-white text-sm font-medium md:px-32">
              <li className="w-full">
                <Link href="/home" className="w-full" onClick={toggleMenu}>
                  Home
                </Link>
              </li>
              <li>
                <Link href="/aboutus" onClick={toggleMenu}>
                  About Us
                </Link>
              </li>
              <li>
                <Link href="/peminjamanbuku" onClick={toggleMenu}>
                  Peminjaman Buku
                </Link>
              </li>
              <li>
                <Link href="/ebook" onClick={toggleMenu}>
                  Ebook
                </Link>
              </li>
              <li>
                <Link href="/audiobook" onClick={toggleMenu}>
                  Audiobook
                </Link>
              </li>
              <li>
                <Link
                  href="/login"
                  className="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-200 transition"
                  onClick={toggleMenu}
                >
                  Login
                </Link>
              </li>
              <li>
                <Navbar />
              </li>
              <li>
                <Link
                  href="/signup"
                  className="px-4 py-2 bg-blue-500 text-white rounded-md text-sm font-medium hover:bg-blue-600 transition"
                  onClick={toggleMenu}
                >
                  Signup
                </Link>
              </li>
            </ul>
          </div>
        )}
      </header>

      {/* Main Content Section */}
      <main className="container mx-auto px-4 mt-28">{children}</main>
    </>
  );
}
