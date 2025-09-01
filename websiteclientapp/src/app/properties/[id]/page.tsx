/**
 * The single property page, where all the detail of a property is shown.
 **/
"use client";
import { formatUSD } from "@/components/properties/propertyGrid";
import { PropertyDTO } from "@/lib/api/apiClient";
import { api } from "@/lib/client";
import { 
  Divider, 
  Image, 
  getKeyValue,
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Pagination,
  Button,
  Link,
  Skeleton,
} from "@heroui/react";
import { useParams } from "next/navigation";
import React from "react";

/**The columns rendere in the traces table. */
const columns = [
  {name: "NAME", uid: "name", sortable: true,},
  {name: "SALE", uid: "dateSale", sortable: true},
  {name: "VALUE", uid: "value", sortable: true },
  {name: "TAX", uid: "tax", sortable: true },
];

export default function PropertyPage() {
  const { id } = useParams<{ id: string }>();

  /**The property object shown in this page. */
  const [property, setProperty] = React.useState<PropertyDTO>();
  /**Whether the page is been loading. */
  const [isLoadingPage, setIsLoadingPage] = React.useState(false);
  /**The max rows to show per page in the table. */
  const rowsPerPage = 10;
  /**The current page number. */
  const [page, setPage] = React.useState(1);

  React.useEffect(() => {
    if (!id) return;

    setIsLoadingPage(true);
    api.property.getProperty(id)
      .then((model: PropertyDTO) => {
        setProperty(model);
      })
      .catch((error) => {
        console.error(error);
      })
      .finally(() => {
        setIsLoadingPage(false);
      });
  }, [id]);
  
  /**The items rendered in the table. */
  const items = React.useMemo(() => {
    const start = (page - 1) * rowsPerPage;
    const end = start + rowsPerPage;

    if (property === undefined) return [];

    return property.propertyTraces.slice(start, end);
  }, [page, property]);
  
  /**The total pages for all the current model. */
  const totalPages = React.useMemo(() => {
    const value = property ? property.propertyTraces.length : 1;
    return Math.ceil(value/10);
  }, [property]);

  return (
    <div>
      <Button radius="lg" as={Link} href="/properties">Go Back</Button>
      {isLoadingPage ?
        <div>
          <div className="space-y-3 p-4">
            <Skeleton className="w-3/5 rounded-lg">
              <div className="h-3 w-3/5 rounded-lg bg-default-200" />
            </Skeleton>
            <Skeleton className="w-4/5 rounded-lg">
              <div className="h-3 w-4/5 rounded-lg bg-default-200" />
            </Skeleton>
            <Skeleton className="w-2/5 rounded-lg">
              <div className="h-3 w-2/5 rounded-lg bg-default-300" />
            </Skeleton>
          </div>
          <Divider></Divider>
          <Skeleton className="w-7xl h-svh rounded-lg m-5">
            <div className="h-3 w-2/5 rounded-lg bg-default-300" />
          </Skeleton>
        </div>
      :
        <div>
          <div className="grid md:grid-cols-2 grid-cols-1 p-2">
            <div>
              <small>Property: {property?.codeInternal}</small>
              <h1>{property?.name}</h1>
              <h3>{property?.address}</h3>
            </div>
            <div className="ml-auto p-2">
              <h2 className="text-6xl">{formatUSD(property?.price)}</h2>
            </div>
          </div>
          <Divider></Divider>
          <div className="m-5">
            <Image src="https://picsum.photos/1200/600/" className="object-cover rounded-xl py-2"/>
          </div>      
          <Divider></Divider>
          <div className="m-5">
            <h2>Traces</h2>
          </div>
          <div className="p-5">
            <Table
              aria-label="Property Traces table"
              isStriped 
              maxTableHeight={800}
              bottomContent={
                <div className="flex w-full justify-center">
                  <Pagination
                    isCompact
                    showControls
                    showShadow
                    color="primary"
                    page={page}
                    total={totalPages}
                    onChange={(page) => setPage(page)}
                  />
                </div>
              }
            >
              <TableHeader columns={columns}>
                {(column) => <TableColumn key={column.uid}>{column.name}</TableColumn>}
              </TableHeader>
              <TableBody items={items}>
                {(item) => (
                  <TableRow key={item.name}>
                    {(columnKey) => <TableCell>{getKeyValue(item, columnKey)}</TableCell>}
                  </TableRow>
                )}
              </TableBody>
            </Table>
          </div>
        </div>
      }
    </div>
  );
}