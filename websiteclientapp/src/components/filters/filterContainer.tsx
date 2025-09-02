/**
 * A component that implements basic filtering for properties.
 * Defining a filter object.
 **/
"use client";
import { 
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  useDisclosure,
  Dropdown, 
  DropdownTrigger, 
  DropdownMenu, 
  DropdownItem, 
  Button, 
  Slider,
  NumberInput,
  Select,
  Input,
  SelectItem,
  Chip,
  Badge
} from "@/lib/heroui-client";
import React from "react";
import type {Selection, SelectedItems, SharedSelection} from "@heroui/react";
import { SearchIcon } from "@/components/icons";
import { PropertyFiltersDTO, PropertyType } from "@/lib/api/apiClient";
import { useDebounce } from "@/hooks/useDebounce";

export type TypeObject = {
  name: string;
  value: string | number;
};

type FilterContainerProps = {
  onFilterChange: (filter: PropertyFiltersDTO) => void
}

/**The year filter types. */
const yearFilterTypes = [
  {
    name: "More Than 20",
    value: 0
  },
  {
    name: "Less Than 5",
    value: 1
  },
  {
    name: "Less Than 10",
    value: 2
  },
  {
    name: "Less Than 20",
    value: 3
  },
];
  
/**The property types values. */
const propertyTypes = [
  {
    name: "Type 1",
    value: 0
  },
  {
    name: "Type 2",
    value: 1
  },
  {
    name: "Type 2",
    value: 2
  },
  {
    name: "Type 4",
    value: 3
  },
  {
    name: "Type 5",
    value: 4
  },
  {
    name: "Type 6",
    value: 5
  },
];

/**The default Min Price Range used for filtering */
export const MIN_PRICERANGE = 1000;
/**The default Max Price Range used for filtering */
export const MAX_PRICERANGE = 10000000;

export default function FilterContainer({ onFilterChange }: FilterContainerProps) {
  /**Whether the bage is visible when there are filters active. */
  const [isFilterBadgeInvisible, setIsFilterBadgeInvisible] = React.useState(true);
  /**The search filter for the name value. */
  const [nameFilterValue, setNameFilterValue] = React.useState("");
  /**The search filter for the name value as a debounce. */
  const [nameDebounce] = useDebounce(nameFilterValue, 300)
  /**The search filter for the address value. */
  const [addressFilterValue, setAddressFilterValue] = React.useState("");
  /**The search filter for the address valueas a debounce. */
  const [addressDebounce] = useDebounce(addressFilterValue, 300)
  /**The price range filter for the property. */
  const [priceRange, setPriceRange] = React.useState<[number, number]>([MIN_PRICERANGE, MAX_PRICERANGE]);
  /**The property types selected to be filtered. */
  const [selectedTypes, setSelectedTypes] = React.useState<number[]>([]);
  /**Whether the clear filters button is disable. */
  const [clearButtonDisable, setClearButtonDisable] = React.useState(false);
  
  const {isOpen, onOpen, onOpenChange, onClose} = useDisclosure();

  /**The selected year label from the year types. */
  const [selectedYearLabel, setSelectedYearLabel] = React.useState<string>("More Than 20");
  /**The selected year value from the year types. */
  const [selectedYearType, setSelectedYearType] = React.useState<number>(0);
  /**Method fire when the year type has changed its value from the select/dropdown. */
  const onYearChange = (keys: SharedSelection) => {
    const values = Array.from(keys).map((k) => Number(k))
    setSelectedYearType(values[0]);
    const label = yearFilterTypes.find(x => x.value === values[0]);
    setSelectedYearLabel(label?.name ? label.name : 'Select a Year');
  };

  React.useEffect(() => {
    applyFilter();
  }, [nameDebounce, addressDebounce]);

  /**Method fire when the search value for the name has changed its value. */
  const onNameSearchChange = React.useCallback((value?: string) => {
    if (value) {
      setNameFilterValue(value);
    } else {
      setNameFilterValue("");
    }
  }, []);
  
  /**Method fire when the search name should clear its value. */
  const onNameClear = React.useCallback(() => {
    setNameFilterValue("");
  }, []);

  /**Method fire when the search value for the address has changed its value. */
  const onAddressSearchChange = React.useCallback((value?: string) => {
    if (value) {
      setAddressFilterValue(value);
    } else {
      setAddressFilterValue("");
    }    
  }, []);
  
  /**Method fire when the search address should clear its value. */
  const onAddressClear = React.useCallback(() => {
    setAddressFilterValue("");
  }, []);

  /**Call to apply the final filter and call the event to save the object. */
  const applyFilter = () => {
    const model : PropertyFiltersDTO = {
      name: nameFilterValue,
      address: addressFilterValue,
      minPrice: priceRange[0],
      maxPrice: priceRange[1],
      yearFilterType: selectedYearType,
      types: selectedTypes as PropertyType[],
      pagination: {pageNumber: 0, pageSize: 0},
    }
    onFilterChange(model);
    onClose();
    setIsFilterBadgeInvisible(false);
  };

  /**Method fire when the selected values for the property type has changed. */
  const onTypeSelectionChange = (keys: SharedSelection) => {
    const values = Array.from(keys).map((k) => Number(k))
    setSelectedTypes(values);
  }

  /**Call to clear all the current filters. */
  const clearFilters = () => {
    const model : PropertyFiltersDTO = {
      name: "",
      address: "",
      minPrice: MIN_PRICERANGE,
      maxPrice: MAX_PRICERANGE,
      types: selectedTypes as PropertyType[],
      yearFilterType: 0,
      pagination: {pageNumber: 0, pageSize: 0},
    }

    setNameFilterValue("");
    setAddressFilterValue("");
    setPriceRange([MIN_PRICERANGE, MAX_PRICERANGE]);
    setSelectedTypes([]);
    onFilterChange(model);
    
    setIsFilterBadgeInvisible(true);
    
    setClearButtonDisable(true);
    setTimeout(() => {
      setClearButtonDisable(false)
    }, 3000);
  } 

  return (
    <div>
      <div className="flex flex-col gap-4 m-2">
        <Input
          isClearable
          variant="bordered"
          placeholder="Search by name..."
          startContent={<SearchIcon />}
          onClear={() => onNameClear()}
          onValueChange={onNameSearchChange}
        />        
        <Input
          isClearable
          variant="bordered"
          placeholder="Search by address..."
          startContent={<SearchIcon />}
          onClear={() => onAddressClear()}
          onValueChange={onAddressSearchChange}
        />
        <div className="flex gap-2">
          <Badge color="success" content="" shape="circle" isInvisible={isFilterBadgeInvisible}>
            <Button color="primary" onPress={onOpen}>Filters</Button>
          </Badge>
          <Button onPress={clearFilters} isDisabled={clearButtonDisable}>Clear Filters</Button>
        </div>

        <Modal 
            isOpen={isOpen} 
            isDismissable={false}
            size="4xl"
            onOpenChange={onOpenChange}
          >
          <ModalContent>
            {(onClose) => (
              <>
                <ModalHeader className="flex flex-col gap-1 m-4 p-4"><h1>Add Filters</h1></ModalHeader>
                <ModalBody>
                  <div className="flex gap-4 items-center">
                    <h2 className="w-full md:w-[30%] p-4">Price Range</h2>
                    <div>
                      <div className="flex gap-2">
                        <NumberInput  
                          label="Minimum Price" 
                          placeholder="0" type="number" 
                          step={100}
                          variant="bordered"
                          formatOptions={{style: "currency", currency: "USD"}}
                          value={priceRange[0]} 
                          onValueChange={(v) => setPriceRange([v ?? 0, priceRange[1]])}
                        />
                        <NumberInput  
                          label="Maximum Price" 
                          placeholder="100" 
                          step={100}
                          variant="bordered"
                          formatOptions={{style: "currency", currency: "USD"}}
                          value={priceRange[1]} 
                          onValueChange={(v) => setPriceRange([priceRange[0], v ?? 0])}
                        />
                      </div>
                      <Slider
                        className="w-full m-2"
                        size="sm"
                        label="Range"
                        defaultValue={priceRange}
                        formatOptions={{style: "currency", currency: "USD"}}
                        maxValue={MAX_PRICERANGE}
                        minValue={MIN_PRICERANGE}
                        step={100}
                        value={priceRange}
                        onChange={(val) => setPriceRange(val as [number, number])}
                      />
                    </div>
                  </div>
                  
                  <div className="flex gap-4 items-center">
                    <h2 className="w-full md:w-[30%] p-4">Age</h2>
                    <Dropdown>
                      <DropdownTrigger>
                        <Button variant="bordered">{selectedYearLabel}</Button>
                      </DropdownTrigger>
                      <DropdownMenu aria-label="Property Type" selectionMode="single" onSelectionChange={onYearChange}>
                        {yearFilterTypes.map((item) => (
                          <DropdownItem key={item.value}>{item.name}</DropdownItem>
                        ))}
                      </DropdownMenu>
                    </Dropdown>
                  </div>
                  
                  <div className="flex gap-4 items-center">
                    <h2 className="w-full md:w-[30%] p-4">Types</h2>
                    <Select
                      classNames={{
                        base: "max-w-xs",
                        trigger: "min-h-12 py-2",
                      }}
                      isClearable
                      onSelectionChange={onTypeSelectionChange}
                      isMultiline={true}
                      items={propertyTypes}
                      label="Select Multiple"
                      labelPlacement="outside"
                      placeholder="No Selection"
                      renderValue={(items: SelectedItems<TypeObject>) => {
                        return (
                          <div className="flex flex-wrap gap-2">
                            {items.map((item) => (
                              <Chip key={item.key}>{item.data?.name}</Chip>
                            ))}
                          </div>
                        );
                      }}
                      selectionMode="multiple"
                      variant="bordered"
                    >
                      {(user) => (
                        <SelectItem key={user.value} textValue={user.name}>
                          <div className="flex gap-2 items-center">
                            <div className="flex flex-col">
                              <span className="text-small">{user.name}</span>
                            </div>
                          </div>
                        </SelectItem>
                      )}
                    </Select>
                  </div>
                </ModalBody>
                <ModalFooter>
                  <Button color="danger" variant="light" onPress={onClose}>
                    Close
                  </Button>
                  <Button color="primary" onPress={applyFilter}>
                    Apply Filters
                  </Button>
                </ModalFooter>
              </>
            )}
          </ModalContent>
        </Modal>
      </div>
    </div>
  )
}