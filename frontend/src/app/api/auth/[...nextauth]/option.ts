import type { NextAuthOptions } from "next-auth"

export const option: NextAuthOptions = {
    providers: [],
    pages: {
        signIn: '/signin'
    }
}