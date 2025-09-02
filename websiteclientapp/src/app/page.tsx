/**
 * The main page where all the app is initialize.
 **/
import { 
  Button, 
  Link, 
  Card, 
  CardHeader, 
  CardBody, 
  CardFooter, 
  Image, 
  Divider 
} from "@/lib/heroui-client";

export default function HomePage() {

  /**The options been displayed in the home page. */
  const options = [
    {
      title: 'Properties',
      route: '/properties',
      imagePath: 'https://picsum.photos/id/178/300/'
    },
    {
      title: 'Owners',
      route: '/owners',
      imagePath: 'https://picsum.photos/id/9/300/'
    }
  ];

  return (
    <div>
      <h1 className="text-center">
        Properties and Owners
      </h1>
      <Divider></Divider>
      <div className="p-5">
        <h2>
          Welcome to PROP-TY.
        </h2>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus gravida quis blandit turpis. Augue neque gravida in fermentum et sollicitudin ac orci. Et sollicitudin ac orci phasellus egestas. Elementum tempus egestas sed sed risus pretium quam vulputate. Interdum velit euismod in pellentesque massa placerat duis ultricies.
          Rhoncus mattis rhoncus urna neque viverra justo nec ultrices dui. Praesent semper feugiat nibh sed pulvinar. Ultrices gravida dictum fusce ut placerat orci nulla pellentesque. Malesuada proin libero nunc consequat interdum varius sit amet. Lectus quam id leo in vitae. Sed viverra tellus in hac habitasse platea dictumst. Vivamus at augue eget arcu. Augue mauris augue neque gravida in.
        </p>
      </div>
      <div className="grid xl:grid-cols-2 md:grid-cols-2 sm:grid-cols-1 grid-cols-1 gap-4 place-items-center">
        {options.map((item, index) => (
          <Card key={index} className="my-4 p-2 w-xs">
            <CardHeader className="pb-0 pt-2 px-4 flex-col items-start">
              <h2 className="font-bold">{item.title}</h2>
              <small className="text-default-500">Description</small>
            </CardHeader>
            <CardBody className="overflow-visible py-2">
              <Image
                alt="Card background"
                className="object-cover rounded-xl"
                src={item.imagePath}
                width={270}
              />
            </CardBody>
            <CardFooter className="text-small justify-between">
              <Button className="w-full" as={Link} color="primary" href={item.route}>View All</Button>
            </CardFooter>
          </Card>
        ))}
      </div>
    </div>
  );
}
