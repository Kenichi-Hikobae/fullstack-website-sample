/**
 * A footer component use in the main layouts.
 **/
import {  Link, Button, Image, Divider } from "@/lib/heroui-client";

export default function Footer() {
  return (
    <div className="w-full bg-slate-950">
      <Divider></Divider>
      <div className="container p-4">
        <div className="flex-column justify-center text-center align-middle">
          <div className="m-2">
            <p className="font-bold text-inherit">© <span>{new Date().getFullYear()}</span> All rights reserved</p>
          </div>
          <div className="p-2 space-x-4">
            <Link color="foreground" href="#">
              Contact Us
            </Link>
            <Link aria-current="page" showAnchorIcon isExternal href="https://github.com/Kenichi-Hikobae?tab=repositories">
              GitHub
            </Link>
          </div>
        </div>
        <div>
        </div>
      </div>
    </div>
  );
}