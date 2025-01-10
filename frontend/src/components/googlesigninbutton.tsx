import { FC, ReactNode } from "react"
import { IoLogoGoogle } from "react-icons/io5"

interface GoogleSignInButtonProps {
    children: ReactNode;
}

const GoogleSignInButton: FC<GoogleSignInButtonProps> = ({children}) => {
    const loginWithGoogle = () => console.log('login with google');

    return (
        <div className="!mt-8">
            <button onClick={loginWithGoogle} type="button" className="w-full py-3 px-4 text-sm tracking-wide rounded-lg bg-gray-50 hover:bg-gray-200 text-black border border-black focus:outline-none flex justify-center items-center">
                <IoLogoGoogle className="mr-5" />
                Sign in with Google
            </button>
        </div>
    )
}