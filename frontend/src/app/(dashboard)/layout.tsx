"use client";

import { useState } from "react";
import Link from "next/link";
import Image from "next/image";
import Navbar from "@/components/Navbar";
import AnimatedScreen from "@/components/animated-screen";


export default function DashboardLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <>
      <AnimatedScreen />
      <Navbar />

      {/* Main Content Section */}
      <main className="container mx-auto px-4 mt-28">{children}</main>
    </>
  );
}
