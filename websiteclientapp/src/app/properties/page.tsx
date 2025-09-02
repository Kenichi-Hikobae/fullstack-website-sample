/**
 * The properties page, where all the properties data is rendered and shown in a datagrid.
 **/
"use client";
import FilterContainer from "@/components/filters/filterContainer";
import PropertyGrid from "@/components/properties/propertyGrid";
import { PropertyFiltersDTO } from "@/lib/api/apiClient";
import { 
  Divider
} from "@/lib/heroui-client";
import React from "react";

export default function PropertiesPage() {
  /**The current filer object to be apply to the grid.*/
  const [filter, setFilter] = React.useState<PropertyFiltersDTO>();

  return (
    <div>
      <h1>Properties</h1>
      <Divider></Divider>
      <div className="my-5">
        <div className="flex flex-col md:flex-row">
          <div className="w-full md:w-[30%] px-2">
            <FilterContainer onFilterChange={setFilter} />
          </div>
          <div className="w-full md:w-[70%] px-2">
            <PropertyGrid filter={filter} />
          </div>
        </div>
      </div>
    </div>
  );
}