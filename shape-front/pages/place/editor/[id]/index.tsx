import { Button, Card, Navbar, Typography } from "@material-tailwind/react";
import { NextPage } from "next";
import Link from "next/link";
import { useRouter } from "next/router";

const Editor: NextPage = () => {
    const router = useRouter();
    const { id } = router.query;

    return (
        <div className="p-2 flex flex-col max-h-screen space-y-5">
            <Navbar className="mx-auto max-w-screen-xl py-2 px-4 lg:px-8 lg:py-4">
                <div className="container mx-auto flex items-center justify-between text-blue-gray-900">
                    <Typography>
                        Editor {id}
                    </Typography>
                    <div className="space-x-5">
                        <Link href={`/place`}>
                            <Button variant="gradient" color="red" size="sm" className="hidden lg:inline-block">
                                <span>Cancel</span>
                            </Button>
                        </Link>
                        <Button variant="gradient" color="green" size="sm" className="hidden lg:inline-block">
                            <span>Save</span>
                        </Button>
                    </div>
                </div>
            </Navbar>
            <div className="grow flex justify-center">
                <div className="border aspect-square w-[50rem] m-auto shadow-lg">
                </div>
                <Card className="border w-[30rem]">
                </Card>
            </div>
        </div>        
        );
};

export default Editor;