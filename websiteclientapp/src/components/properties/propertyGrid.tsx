/**
 * A component that implements a datagrid using cards and pagination to show the properties data.
 **/
import { 
  Button, 
  Link, 
  Card, 
  CardHeader, 
  CardBody, 
  CardFooter, 
  Image, 
  Pagination, 
  Spinner, 
  Skeleton, 
  Chip, 
  Divider, 
  PressEvent 
} from "@/lib/heroui-client";
import React from "react";
import { api } from "@/lib/client";
import { PropertiesLoadedCountDTO, PropertyDTO, PropertyFiltersDTO } from "@/lib/api/apiClient";
import { MAX_PRICERANGE, MIN_PRICERANGE } from "../filters/filterContainer";

type PropertyGridProps = {
  filter: PropertyFiltersDTO | undefined
}

/**Function that format the currency to USD dollars. */
export function formatUSD(amount: number | undefined): string {
  if (amount === undefined) return "0";

  return new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  }).format(amount);
}

export default function PropertyGrid({ filter }: PropertyGridProps) {
  /**Whether the grid is loading or not. */
  const [isGridsLoading, setIsGridLoading] = React.useState(true);
  /**Whether the datagrid is been shown or not. */
  const [isShowingData, setIsShowingData] = React.useState(false);
  /**The current page number value. */
  const [page, setPage] = React.useState(1);
  /**The page size value for the total of elements render for each page. */
  const [pageSize, setPageSize] = React.useState(10);

  /**Event called when the page changed. */
  const onPaginationChange = React.useCallback((page: number) => {
    setPage(page);
    setIsGridLoading(true);
  }, []);
  
  /**Whether the properties has ben loaded correctly. */
  const [propertiesLoaded, setPropertiesLoaded] = React.useState(0);
  /**The properties loaded as obejects to be be shown in the grid. */
  const [properties, setProperties] = React.useState<PropertyDTO[]>([]);
  React.useEffect(() => {
    const filterModel: PropertyFiltersDTO = filter === undefined ? 
    {
      name: "",
      address: "",
      minPrice: MIN_PRICERANGE,
      maxPrice: MAX_PRICERANGE,
      types: [],
      yearFilterType: 0,
      pagination: {
        pageNumber: page,
        pageSize: 10
      }
    } : filter;

    filterModel.pagination = {
      pageNumber: page,
      pageSize: 10
    }
    api.property.getProperties(filterModel)
      .then((model: PropertiesLoadedCountDTO) => { 
        setProperties(model.properties);
        setPropertiesLoaded(model.totalCount);
        setIsShowingData(model.totalCount > 0);
      })
      .catch((error) => {
        console.error(error);
        setProperties([]);
        setPropertiesLoaded(0);
        setIsShowingData(false);
      })
      .finally(() => {
        setIsGridLoading(false);
      });;
  }, [isGridsLoading]);
  
  React.useEffect(() => {
    setIsGridLoading(true);
  }, [filter]);

  /**The total properties currently loaded. */
  const [totalProperties, setTotalProperties] = React.useState(1);
  React.useEffect(() => {
    api.property.getTotalCount().then((model) => { 
      setTotalProperties(model)
    });
  }, []);

  /**The toal pages to render at the moment. */
  const totalPages = React.useMemo(() => {
    return Math.ceil(propertiesLoaded/pageSize);
  }, [propertiesLoaded, pageSize]);

  /**Whether the refresh button is disable dor not. */
  const [refreshButtonDisable, setRefreshButtonDisable] = React.useState(false);
  const onRefresh = (ev: PressEvent) => {
    setIsGridLoading(true);
    setRefreshButtonDisable(true);

    setTimeout(() => {
      setRefreshButtonDisable(false)
    }, 3000);
  };

  return (
    <div>
      {isShowingData ?
        <div>
          <div className="grid grid-cols-1 lg:grid-cols-2 md:grid-cols-1 sm:grid-cols-1">
            <div className="m-2">
              <Button className="mx-2" onPress={onRefresh} isDisabled={refreshButtonDisable}>Refresh</Button>
              <Chip color="default" variant="dot">
                Total Properties: {totalProperties}
              </Chip>
              <Chip color="primary" variant="dot">
                Loaded: {propertiesLoaded}
              </Chip>
            </div>
            <div className="mb-4 lg:ml-auto md:mx-auto mx-auto">
              <Pagination isCompact showControls initialPage={1} total={totalPages} page={page} onChange={onPaginationChange}/>
            </div>
          </div>
          <Divider></Divider>
          <div className="max-h-dvh w-full overflow-y-auto p-4 space-y-2">
            {isGridsLoading ?
              <div className="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-2 grid-cols-1 gap-4 place-items-center">
                {/* <Spinner classNames={{label: "text-foreground mt-4"}} label="Loading" variant="spinner" /> */}
                {Array.from({ length: 6 }, (_, i) => (
                  <Card key={i} className="w-[290px] space-y-5 p-4" radius="lg">
                    <div className="space-y-3">
                      <Skeleton className="w-3/5 rounded-lg">
                        <div className="h-3 w-3/5 rounded-lg bg-default-200" />
                      </Skeleton>
                    </div>
                    <Skeleton className="rounded-lg">
                      <div className="h-24 rounded-lg bg-default-300" />
                    </Skeleton>
                    <div className="space-y-3">
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
                  </Card>
                ))}
              </div>
              :
              <div className="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-2 grid-cols-1 gap-4 place-items-center">
                {properties.map((item, index) => (
                  <Card key={item.id} className="my-4 p-2">
                    <CardHeader className="pb-0 pt-2 px-4 flex-col items-start">
                      <h2 className="font-bold">{item.name}</h2>
                      <small className="text-default-500">Type - {Number(item.propertyType) + 1}</small>
                    </CardHeader>
                    <CardBody className="overflow-visible py-2">
                      <Image
                        alt="Property Image"
                        className="object-cover rounded-xl py-2"
                        // src={item.propertyImages[0].file}
                        src="https://picsum.photos/400/"
                        width={300}
                        height={300}
                      />
                      <h2 className="font-bold">{formatUSD(item.price)}</h2>
                      <h4 className="text-default-500">{item.address}</h4>
                      <p>Description</p>
                      <small className="text-default-500">{item.year}</small>
                    </CardBody>
                    <CardFooter className="text-small justify-between">
                      <Button className="w-full" as={Link} color="primary" href={"/properties/" + item.id}>View Details</Button>
                    </CardFooter>
                  </Card>
                ))}
              </div>
            }
            <div className="mb-4 flex justify-center">
              <Pagination isCompact showControls initialPage={1} total={totalPages} page={page} onChange={onPaginationChange}/>
            </div>
          </div>
        </div>
        :    
        <div className="h-50 flex justify-center">
            <Chip color="primary" variant="shadow" radius="sm" className="p-5 self-center">
              <h1>No Properties...</h1>
            </Chip>
        </div>
      }
    </div>
  );
}