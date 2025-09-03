/**
 * Base Layout class use in all the application.
 * Contains the initialization of the containers of the app.
 **/
import type { Metadata } from "next";
import { Roboto } from 'next/font/google'
import "./globals.css";

import { Providers } from './providers';
import Header from "@/components/layouts/header";
import Footer from "@/components/layouts/footer";

/**
 * The roboto font comming from google and NextJs source
 */
const roboto = Roboto({
  weight: '400',
  subsets: ['latin'],
});

/**Metadata for the application. */
export const metadata: Metadata = {
  title: "Property App",
  description: "A property website to review owners and its properties.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
        className={`${roboto.className} antialiased`}
      >
        <Providers themeProps={{ attribute: "class", defaultTheme: "dark" }}>
          <header>
            <Header />
          </header>
          <main className="container mx-auto">
              {children}
          </main>
          <footer>
            <Footer />
          </footer>
        </Providers>
      </body>
    </html>
  );
}
