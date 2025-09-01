import type { Config } from "tailwindcss";
import { heroui } from "@heroui/react";

const config: Config = {
  content: [
    "./src/**/*.{js,ts,jsx,tsx,mdx}",
    "./node_modules/@heroui/theme/dist/**/*.{js,ts,jsx,tsx}", 
  ],
  theme: {
    container: {
      center: true,
      padding: "1rem",
      screens: {
        sm: "640px",
        md: "768px",
        lg: "1024px",
        xl: "1280px",
        "2xl": "1400px",
      },
    },
    extend: {
      colors: {
        background: 'var(--background)',
        foreground: 'var(--foreground)',
      },
      fontFamily: {
        sans: ["var(--font-sans)"],
        mono: ["var(--font-mono)"],
      },
      fontSize: {
        h1: '2rem',
        h2: '1.5rem',
        h3: '1.3rem',        
        h4: '1rem',
      },
    },
  },
  darkMode: "class",
  plugins: [
    heroui({
      themes: {        
        light: {
          colors: {
            background: "#eeeeeeff",
            foreground: "#000000",
            primary: {
              DEFAULT: "#0ebebb",
              foreground: "#000000",
            },
            focus: "#0ebebb",
          },
        },
        dark: {
          colors: {
            background: "#0a0f1a",
            foreground: "#ecedeeff",
            primary: {
              DEFAULT: "#0ebebb",
              foreground: "#000000",
            },
            content1: "#0e1525",
            content2: "#18243f",
            content3: "#1e2d4d",
            content4: "#30487d",
            default: {
              DEFAULT: "#0f1b33ff",
              "50": "#0a0f1a",              
              "100": "#0a0f1a",
            },
            focus: "#0ebebb",
          },
        },
      },
    }),
  ],
};

export default config;