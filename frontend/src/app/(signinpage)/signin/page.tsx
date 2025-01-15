"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { IoPersonSharp, IoEyeSharp } from "react-icons/io5";
import Link from "next/link";
import Image from "next/image";

const SigninPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [isMounted, setIsMounted] = useState(false); // To track if the component is mounted
    const router = useRouter();
    
    useEffect(() => {
        setIsMounted(true);
    }, []);

    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/login`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    userEmail: email,
                    password: password,
                }),
                credentials: "include",
            });

            if (!response.ok) {
                const errorData = await response.json();
                setErrorMessage(errorData.message || "Login failed");
                return;
            }

            const data = await response.json();
            console.log(data.message);

            // Redirect to home after successful login
            router.push("/home"); // Change '/home' to your actual home route
        } catch (error) {
            if (error instanceof Error) {
                console.error("Login error:", error.message);
                setErrorMessage(error.message);
            } else {
                console.error("Unexpected error:", error);
                setErrorMessage("An unexpected error occurred.");
            }
        }
    };

    if (!isMounted) {
        return null; // Prevents using router before component is mounted
    }

    return (
        <div className="bg-gray-50 font-[sans-serif]">
            <div className="min-h-screen flex flex-col items-center justify-center py-6 px-4">
                <div className="max-w-md w-full">
                    <div className="flex justify-center items-center mb-5">
                        <Image
                            src="/logo.jpeg"
                            alt="logo"
                            width={100}
                            height={100}
                            className="rounded-full w-16 h-16 mr-2"
                        />
                        <span className="text-xl font-bold text-gray-700 ml-2">TamanBaca</span>
                    </div>

                    <div className="p-8 rounded-2xl bg-white shadow">
                        <h2 className="text-gray-800 text-center text-2xl font-bold">Sign in</h2>
                        <form className="mt-8 space-y-4" onSubmit={handleLogin}>
                            <div>
                                <label className="text-gray-800 text-sm mb-2 block">User name</label>
                                <div className="relative flex items-center">
                                    <input name="email" type="email" required className="w-full text-gray-800 text-sm border border-gray-300 px-4 py-3 rounded-md outline-blue-600" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
                                    <IoPersonSharp className="absolute right-4" />
                                </div>
                            </div>

                            <div>
                                <label className="text-gray-800 text-sm mb-2 block">Password</label>
                                <div className="relative flex items-center">
                                    <input name="password" type="password" required className="w-full text-gray-800 text-sm border border-gray-300 px-4 py-3 rounded-md outline-blue-600" placeholder="Enter password" value={password} onChange={(e) => setPassword(e.target.value)} />
                                    <IoEyeSharp className="absolute right-4"/>
                                </div>
                            </div>

                            <div className="flex flex-wrap items-center justify-between gap-4">
                                <div className="flex items-center">
                                    <input id="remember-me" name="remember-me" type="checkbox" className="h-4 w-4 shrink-0 text-blue-600 focus:ring-blue-500 border-gray-300 rounded" />
                                    <label htmlFor="remember-me" className="ml-3 block text-sm text-gray-800">
                                        Remember me
                                    </label>
                                </div>
                                <div className="text-sm">
                                    <Link href="/forgotpassword" className="text-blue-600 hover:underline">
                                        Forgot your password?
                                    </Link>
                                </div>
                            </div>
                            {errorMessage && (
                                <div className="text-red-600 text-sm text-center">{errorMessage}</div>
                            )}
                            <div className="!mt-8">
                                <button type="submit" className="w-full py-3 px-4 text-sm tracking-wide rounded-lg text-white bg-blue-600 hover:bg-blue-700 focus:outline-none">
                                    Sign in
                                </button>
                            </div>
                            <p className="text-gray-800 text-sm !mt-8 text-center">Dont have an account? 
                                <Link href="/signup" className="text-blue-600 hover:underline ml-1 whitespace-nowrap font-semibold">Register here
                                </Link>
                            </p>
                        </form>
                        {/* <div className="!mt-8">
                            <button
                                type="button"
                                onClick={() => signIn("google")}
                                className="w-full py-3 px-4 text-sm tracking-wide rounded-lg bg-gray-50 hover:bg-gray-200 text-black border border-black focus:outline-none flex justify-center items-center"
                            >
                                <IoLogoGoogle className="mr-5" />
                                Sign in with Google
                            </button>
                        </div> */}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SigninPage;
