"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import Image from "next/image";
import Navbar from "@/components/Navbar";
import AnimatedScreen from "@/components/animated-screen";


export default function SingInLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const [message, setMessage] = useState('');
  useEffect(() => {
    (
      async () => {
        try {
          const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/user`, {
            credentials: 'include'
          });
  
          const content = await response.json();
  
          setMessage(`Hi ${content.userEmail}`);
        }
        catch(e) { 
          setMessage(`You are not logged in`);
        }
      }
    )
  })
  return (
    <>
      
      {/* Main Content Section */}
      <main className="">{message}{children}</main>
    </>
  );
}
