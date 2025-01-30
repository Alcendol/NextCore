"use client";

import React, { SyntheticEvent, useState, useEffect } from "react";
import Link from "next/link";
import {IoLogoGoogle, IoPersonSharp } from "react-icons/io5";
import { useRouter } from "next/navigation";

const SignupPage = () => {
  const [isClient, setIsClient] = useState(false);
  const [userId, setUserId] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [userEmail, setUserEmail] = useState("");
  const [userPhone, setUserPhone] = useState("");
  const [password, setPassword] = useState("");
  const [imageKtp, setImageKtp] = useState<File | null>(null);
  const [loading, setLoading] = useState<boolean>(false); 

  const router = useRouter();

  useEffect(() => {
    setIsClient(true);
  }, []);

  const handleSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();

    if (!imageKtp) {
      alert("Please upload a valid KTP image.");
      return;
    }

    const formData = new FormData();
    formData.append("userId", userId);
    formData.append("firstName", firstName);
    formData.append("lastName", lastName);
    formData.append("userEmail", userEmail);
    formData.append("userPhone", userPhone);
    formData.append("password", password);
    formData.append("imageKtp", imageKtp);
    setLoading(true);
    try {
      const response = await fetch(`http://localhost:5259/api/register`, {
        method: "POST",
        body: formData,
      });
      console.log(formData);
      if (!response.ok) {
        throw new Error(`Error: ${response.status} - ${response.statusText}`);
      }
      await response.json();
      setLoading(false);
      router.push("/signin");
    } catch (error) {
      console.error("Registration error:", error);
      alert("Registration failed. Please try again.");
      setLoading(false);
    }
  };

  if (!isClient) {
    return null; // Prevent rendering on the server
  }

  return (
    <div className="container mx-auto mt-28 pb-5">
      <div className="w-full mb-5">
        <span className="text-3xl font-bold w-full">Sign Up Page</span>
      </div>
      <Link href="/signin">
        <span className="font-sans text-sm container mx-auto text-gray-400 hover:text-black hover:underline">
          Back to Sign-in Page
        </span>
      </Link>
      <div className="container mx-auto px-4 mt-5 rounded-2xl bg-white shadow border border-black">
        <div className="my-10 px-4 lg:px-6">
          <form className="mt-8 space-y-4" onSubmit={handleSubmit}>
            {[ // Render form fields dynamically
              { label: "NIK", name: "userId", type: "text", state: setUserId },
              { label: "First Name", name: "firstName", type: "text", state: setFirstName },
              { label: "Last Name", name: "lastName", type: "text", state: setLastName },
              { label: "Email", name: "userEmail", type: "email", state: setUserEmail },
              { label: "Phone Number", name: "userPhone", type: "tel", state: setUserPhone },
              { label: "Password", name: "password", type: "password", state: setPassword },
            ].map(({ label, name, type, state }, idx) => (
              <div key={idx}>
                <label htmlFor={name} className="text-gray-800 text-sm mb-2 block">
                  {label}
                </label>
                <div className="relative flex items-center">
                  <input
                    id={name}
                    name={name}
                    type={type}
                    required
                    className="w-full text-gray-800 text-sm border border-gray-300 px-4 py-3 rounded-md outline-blue-600"
                    placeholder={`Enter your ${label}`}
                    onChange={(e) => state(e.target.value)}
                  />
                  <IoPersonSharp className="absolute right-4" />
                </div>
              </div>
            ))}
            <div>
              <label htmlFor="imageKtp" className="text-gray-800 text-sm mb-2 block">
                KTP Image
              </label>
              <div className="relative flex items-center">
                <input
                  id="imageKtp"
                  name="imageKtp"
                  type="file"
                  required
                  aria-label="KTP Image"
                  onChange={(e) => setImageKtp(e.target.files?.[0] || null)}
                  className="w-full text-gray-800 text-sm border border-gray-300 px-4 py-3 rounded-md outline-blue-600"
                />
              </div>
            </div>
            <div className="!mt-8">
              <button
                type="submit"
                className="w-full py-3 px-4 text-sm tracking-wide rounded-lg text-white bg-blue-600 hover:bg-blue-700 focus:outline-none"
                disabled={loading}
              >
                {loading? "Loading..." : "Sign up"}
              </button>
            </div>
            <p className="text-gray-800 text-sm !mt-8 text-center">Or</p>
          </form>
          <div className="!mt-8">
            <button
              type="button"
              className="w-full py-3 px-4 text-sm tracking-wide rounded-lg bg-gray-50 hover:bg-gray-200 text-black border border-black focus:outline-none flex justify-center items-center"
            >
              <IoLogoGoogle className="mr-5" />
              Sign in with Google
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default SignupPage;
