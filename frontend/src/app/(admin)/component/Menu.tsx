"use client";

import { useState } from "react";
import Link from "next/link";
import { IoCaretForward, IoCaretDown } from "react-icons/io5";


const menuDashboards = [
  {
    title: "Admin Dashboard",
    items: [
      {
        label: "Admin Dashboard",
        href: "/admindashboard",
      },
    ],
  },
];

const menuEBook = [
    {
      title: "Input Data E-Book",
      items: [
        {
          label: "Author",
          href: "/admindashboard/author",
        },
        {
          label: "Book",
          href: "/admindashboard/ebook",
        },
        {
          label: "Publishers",
          href: "/admindashboard/publisher",
        },
        {
          label: "Genre",
          href: "/admindashboard/genre",
        },
      ],
    },
  ];

const menuInputData = [
  {
    title: "Input Data Peminjaman Buku",
    items: [
      {
        label: "Author",
        href: "/admindashboard/author",
      },
      {
        label: "Book",
        href: "/admindashboard/book",
      },
      {
        label: "Publishers",
        href: "/admindashboard/publisher",
      },
      {
        label: "Genre",
        href: "/admindashboard/genre",
      },
    ],
  },
];

const Menu = () => {
  const [openMenus, setOpenMenus] = useState<{ [key: string]: boolean }>({});

  const handleMenuClick = (title: string) => {
    setOpenMenus((prev) => ({
      ...prev,
      [title]: !prev[title],
    }));
  };
  const [activeMenu, setActiveMenu] = useState<string | null>(null);

  return (
    <div className="mt-10 text-sm">
        {menuDashboards.map((menu) => (
            <div className="flex flex-col gap-2" key={menu.title}>
                <div className="flex justify-between items-center" onClick={() => handleMenuClick(menu.title)}>
                    <span
                        className="hidden lg:block text-gray-400 my-4 cursor-pointer font-sans font-bold">
                        {menu.title}
                    </span>
                    <span
                        className={`transition-transform duration-300 cursor-pointer ${
                        openMenus[menu.title] ? "rotate-90" : "rotate-0"
                        }`}
                    >
                        <IoCaretForward className="w-3 h-3" />
                    </span>
                </div>
                {openMenus[menu.title] && (
                    <div className="flex flex-col pl-6">
                        {menu.items.map((item) => (
                            <Link
                            href={item.href}
                            key={item.label}
                            className="flex items-center justify-center lg:justify-start gap-4 text-gray-500 py-2"
                            >
                            <span className="hidden lg:block">{item.label}</span>
                            </Link>
                        ))}
                    </div>
                )}
            </div>
        ))}

        {menuInputData.map((menu) => (
            <div className="flex flex-col gap-2" key={menu.title}>
                <div className="flex justify-between items-center" onClick={() => handleMenuClick(menu.title)}>
                    <span
                        className="lg:block text-gray-400 my-4 cursor-pointer font-sans font-bold">
                        {menu.title}
                    </span>
                    <span
                        className={`transition-transform duration-300 cursor-pointer ${
                        openMenus[menu.title] ? "rotate-90" : "rotate-0"
                        }`}
                    >
                        <IoCaretForward className="w-3 h-3" />
                    </span>
                </div>
                {openMenus[menu.title] && (
                    <div className="flex flex-col pl-6">
                        {menu.items.map((item) => (
                            <Link
                            href={item.href}
                            key={item.label}
                            className="flex items-center justify-center lg:justify-start gap-4 text-gray-500 py-2"
                            >
                            <span className="hidden lg:block">{item.label}</span>
                            </Link>
                        ))}
                    </div>
                )}
            </div>
        ))}

        {menuEBook.map((menu) => (
            <div className="flex flex-col gap-2" key={menu.title}>
                <div className="flex justify-between items-center" onClick={() => handleMenuClick(menu.title)}>
                    <span
                        className="hidden lg:block text-gray-400 my-4 cursor-pointer font-sans font-bold">
                        {menu.title}
                    </span>
                    <span
                        className={`transition-transform duration-300 cursor-pointer ${
                        openMenus[menu.title] ? "rotate-90" : "rotate-0"
                        }`}
                        >
                        <IoCaretForward className="w-3 h-3" />
                    </span>
                </div>
                {openMenus[menu.title] && (
                    <div className="flex flex-col pl-6">
                        {menu.items.map((item) => (
                            <Link
                                href={item.href}
                                key={item.label}
                                className="flex items-center justify-center lg:justify-start gap-4 text-gray-500 py-2"
                                >
                                <span className="hidden lg:block">{item.label}</span>
                            </Link>
                        ))}
                    </div>
                )}
            </div>
        ))}
    </div>
  );
};

export default Menu;
