import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import { Roboto } from 'next/font/google'
import "../globals.css";
import Link from "next/link";
import Image from "next/image";
import Menu from "./component/Menu";
import NavbarAdmin from "./component/NavbarAdmin";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

const roboto = Roboto({
  weight: '400',
  subsets: ['latin'],
})

export default function AdminLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return ( 
    <div className="h-screen flex">
      <div className="w-[14%] md:w-[8%] lg:w-[16%] xl:w-[14%] p-4">
        <Link href="/" className="flex items-center justify-center gap-2 lg:justify-start">
          <Image src="/logo.jpeg" alt="logo" width={20} height={20}></Image>
          <span className="hidden lg:block">TamanBaca</span>
        </Link>
        <Menu/>
      </div>
      <div className="w-[86%] md:w-[92%] lg:w-[84%] xl:w-[86%] bg-[#F7F8FA] overflow-scroll">
        <NavbarAdmin />
        {children}
      </div>
    </div>
  );
}
