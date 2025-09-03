/**
 * A header component use in the main layouts.
 **/
"use client";
import { Navbar, NavbarBrand, NavbarContent, NavbarItem, Link, Button, Divider, NavbarMenuToggle, NavbarMenu, NavbarMenuItem } from "@/lib/heroui-client";
import { LogoIcon } from "@/components/icons";
import { usePathname } from "next/navigation";
import { ThemeSwitcher } from "@/components/themeSwicher";
import React from "react";

export default function Header() {
  /**Whether the left menu is open or shown. */
  const [isMenuOpen, setIsMenuOpen] = React.useState(false);

  const pathname = usePathname();
  /**The items render in the nav bar. */
  const headerItems = [
    { 
      label: "Home", 
      href: "/",
    },
    { 
      label: "Properties", 
      href: "/properties",
    },
    { 
      label: "Owners", 
      href: "/owners",
    },
  ];

  return (
    <div>
      <Navbar isBordered position="sticky" className="p-2" shouldHideOnScroll={false}>
        <NavbarContent className="flex gap-4" justify="start">
          <Link href="/" color="foreground">
            <LogoIcon className="w-10 h-10 m-5"></LogoIcon>
            <p className="font-bold text-inherit">PROP-TY</p>
          </Link>
          <Divider className="mx-4" orientation="vertical" />
          <div className="hidden sm:flex space-x-4">
            {headerItems.map((item) => (
              <NavbarItem key={item.href} isActive={pathname === item.href}>
                <Link
                  color={pathname === item.href ? "primary" : "foreground"}
                  underline="hover"
                  href={item.href}
                  >
                  {item.label}
                </Link>
              </NavbarItem>
            ))}
          </div>
        </NavbarContent>
        <NavbarContent justify="end">
          <NavbarItem>
            <ThemeSwitcher />
          </NavbarItem>
          <NavbarMenuToggle
            aria-label={isMenuOpen ? "Close menu" : "Open menu"}
            className="sm:hidden"
          />
        </NavbarContent>
        
        <NavbarMenu>
          {headerItems.map((item, index) => (
            <NavbarMenuItem key={`${item}-${index}`} className="m-2">
              <Link
                className="flex justify-self-end"
                color={
                  index === 2 ? "primary" : index === headerItems.length - 1 ? "danger" : "foreground"
                }
                href={item.href}
                size="lg"
              >
                {item.label}
              </Link>
            </NavbarMenuItem>
          ))}
        </NavbarMenu>
      </Navbar>
    </div>
  );
}