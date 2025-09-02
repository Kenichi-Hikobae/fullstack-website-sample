/**
 * The container class that initialize the heroUI provider and the basic theme setup.
 **/
"use client"; 
import { useEffect, useState } from 'react';
import { HeroUIProvider } from "@heroui/react";
import { ThemeProvider as NextThemesProvider } from "next-themes";
import type { ThemeProviderProps } from "next-themes";
import { useRouter } from 'next/navigation';
export * from "@heroui/react";

export interface ProvidersProps {
  children: React.ReactNode;
  themeProps?: ThemeProviderProps;
}

export function Providers({ children, themeProps }: ProvidersProps) {
  const router = useRouter();

  const [isClient, setIsClient] = useState(false);
  useEffect(() => {
    setIsClient(true);
  }, []);

  if (!isClient) return null;

  return (
    <HeroUIProvider
      navigate={router.push}
      useHref={(href) => href}
    >
      <NextThemesProvider {...themeProps}>
        {children}
      </NextThemesProvider>
    </HeroUIProvider>
  );
}