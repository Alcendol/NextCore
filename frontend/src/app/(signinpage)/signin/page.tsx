import { IoPersonSharp, IoEyeSharp, IoLogoGoogle } from "react-icons/io5"
import Link from "next/link"
import Image from "next/image"

const SigninPage = () => {
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
                        <form className="mt-8 space-y-4">
                            <div>
                                <label className="text-gray-800 text-sm mb-2 block">User name</label>
                                <div className="relative flex items-center">
                                    <input name="username" type="text" required className="w-full text-gray-800 text-sm border border-gray-300 px-4 py-3 rounded-md outline-blue-600" placeholder="Enter user name" />
                                    <IoPersonSharp className="absolute right-4" />
                                </div>
                            </div>

                            <div>
                                <label className="text-gray-800 text-sm mb-2 block">Password</label>
                                <div className="relative flex items-center">
                                    <input name="password" type="password" required className="w-full text-gray-800 text-sm border border-gray-300 px-4 py-3 rounded-md outline-blue-600" placeholder="Enter password" />
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

                            <div className="!mt-8">
                                <button type="button" className="w-full py-3 px-4 text-sm tracking-wide rounded-lg text-white bg-blue-600 hover:bg-blue-700 focus:outline-none">
                                    Sign in
                                </button>
                            </div>
                            <p className="text-gray-800 text-sm !mt-8 text-center">Don't have an account? <a href="javascript:void(0);" className="text-blue-600 hover:underline ml-1 whitespace-nowrap font-semibold">Register here</a></p>
                        </form>
                        <div className="!mt-8">
                            <button type="button" className="w-full py-3 px-4 text-sm tracking-wide rounded-lg bg-gray-50 hover:bg-gray-200 text-black border border-black focus:outline-none flex justify-center items-center">
                                <IoLogoGoogle className="mr-5" />
                                Sign in with Google
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default SigninPage