"use client";

import { useState } from "react";
import Link from "next/link";
import { IoCaretForward, IoCaretDown, IoMenu, IoClose } from "react-icons/io5";

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
        label: "E-Book",
        href: "/admindashboard/ebook",
      },
    ],
  },
];

const menuInputData = [
  {
    title: "Input Data Peminjaman Buku",
    items: [
      {
        label: "Book",
        href: "/admindashboard/book",
      },
    ],
  },
];

const menuInputLain = [
  {
    title: "Input Data Lain-Lain",
    items: [
      {
        label: "Author",
        href: "/admindashboard/author",
      },
      {
        label: "Publishers",
        href: "/admindashboard/publishers",
      },
      {
        label: "Genre",
        href: "/admindashboard/genre",
      },
    ],
  },
];

const menuItems = [
  {
    title: "Admin Dashboard",
    href: "/admindashboard",
  },
  {
    title: "Input Data E-Book",
    href: "/admindashboard/ebook",
  },
  {
    title: "Input Data Peminjaman Buku",
    href: "/admindashboard/book",
  },
  {
    title: "Input Data Author",
    href: "/admindashboard/author",
  },
  {
    title: "Input Data Publishers",
    href: "/admindashboard/publishers",
  },
  {
    title: "Input Data Genre",
    href: "/admindashboard/genre",
  },
  {
    title: "Logout",
    href: "/logout",
  },
];

const Menu = () => {
  const [openMenus, setOpenMenus] = useState<{ [key: string]: boolean }>({});
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);

  const handleMenuClick = (title: string) => {
    setOpenMenus((prev) => ({
      ...prev,
      [title]: !prev[title],
    }));
  };

  return (
    <div className="mt-10 text-sm">
      <div className="flex lg:hidden justify-between items-center mb-4">
        <button onClick={() => setIsMobileMenuOpen(true)} className="text-gray-500 focus:outline-none">
          <IoMenu className="w-6 h-6" />
        </button>
      </div>

      {isMobileMenuOpen && (
        <div className="fixed bg-gray-800 rounded-lg bg-opacity-95 text-white z-50 flex flex-col items-center justify-center p-10 lg:hidden">
          <button onClick={() => setIsMobileMenuOpen(false)} className="absolute top-6 right-6 text-white focus:outline-none">
            <IoClose className="w-8 h-8" />
          </button>
          <div className="flex flex-col gap-6 text-lg">
            {menuItems.map((menu) => (
              <Link
                href={menu.href}
                key={menu.title}
                className="hover:underline"
                onClick={() => setIsMobileMenuOpen(false)}
              >
                {menu.title}
              </Link>
            ))}
          </div>
        </div>
      )}
      
      <div className={`${isMobileMenuOpen ? "block" : "hidden"} lg:block bg-white lg:bg-transparent`}>
        {menuDashboards.map((menu) => (
          <div className="flex flex-col gap-2" key={menu.title}>
            <div
              className="flex justify-between items-center"
              onClick={() => handleMenuClick(menu.title)}
            >
              <span className="hidden lg:block text-gray-400 my-4 cursor-pointer font-sans font-bold">
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
                    className="flex items-center justify-center lg:justify-start gap-4 text-gray-500 py-2 hover:bg-gray-300"
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
            <div
              className="flex justify-between items-center"
              onClick={() => handleMenuClick(menu.title)}
            >
              <span className="hidden lg:block text-gray-400 my-4 cursor-pointer font-sans font-bold">
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
                    className="flex items-center justify-center lg:justify-start gap-4 text-gray-500 py-2 hover:bg-gray-300"
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
            <div
              className="flex justify-between items-center"
              onClick={() => handleMenuClick(menu.title)}
            >
              <span className="hidden lg:block text-gray-400 my-4 cursor-pointer font-sans font-bold">
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
                    className="flex items-center justify-center lg:justify-start gap-4 text-gray-500 py-2 hover:bg-gray-300"
                  >
                    <span className="hidden lg:block">{item.label}</span>
                  </Link>
                ))}
              </div>
            )}
          </div>
        ))}

        {menuInputLain.map((menu) => (
          <div className="flex flex-col gap-2" key={menu.title}>
            <div
              className="flex justify-between items-center"
              onClick={() => handleMenuClick(menu.title)}
            >
              <span className="hidden lg:block text-gray-400 my-4 cursor-pointer font-sans font-bold">
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
                    className="flex items-center justify-center lg:justify-start gap-4 text-gray-500 py-2 hover:bg-gray-300"
                  >
                    <span className="hidden lg:block">{item.label}</span>
                  </Link>
                ))}
              </div>
            )}
          </div>
        ))}
        <div className="flex flex-col">
          <Link
              href={"/logout"}
              className="lg:justify-start gap-4 text-gray-500 py-2 hover:bg-gray-100 my-2"
              >
              <span className="hidden lg:block font-sans text-gray-400 font-bold">Logout</span>
          </Link>
        </div>
        
      </div>
    </div>
  );
};

export default Menu;
