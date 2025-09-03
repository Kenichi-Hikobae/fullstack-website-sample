/**
 * The owners page, where all the owners data is rendered and shown in a datatable.
 **/
"use client";
import React from "react";
import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  getKeyValue,
  Pagination,
  Divider,
} from "@heroui/react";
import { api } from "@/lib/client";
import { OwnerDTO } from "@/lib/api/apiClient";

/**The columns t obe displayed in the table.*/
export const ownerColumnsTable = [
  {name: "ID", uid: "id", sortable: true},
  {name: "NAME", uid: "name", sortable: true},
  {name: "EMAIL", uid: "email", sortable: true},
  {name: "ADDRESS", uid: "address", sortable: true},
  {name: "PHOTO", uid: "photo", sortable: true},
  {name: "BIRTHDAY", uid: "birthday", sortable: true},
  {name: "ACTIONS", uid: "actions"},
];

export default function OwnersPage () {
  /**The page number. */
  const [page, setPage] = React.useState(1);
  /**The rows render per page. */
  const rowsPerPage = 10;
  /**The owners model to be loaded in the table.*/
  const [owners, setOwners] = React.useState<OwnerDTO[]>([]);

  /**The total pages in the table. */
  const totalPages = React.useMemo(() =>{
    return Math.ceil(owners.length / rowsPerPage);
  }, [owners]);

  /**The items rendered in the datatable.*/
  const items = React.useMemo(() => {
    const start = (page - 1) * rowsPerPage;
    const end = start + rowsPerPage;

    return owners.slice(start, end);
  }, [page, owners]);

  React.useEffect(() => {
    api.owner.getAllOwners()
      .then(setOwners);
  }, []);

  return (
    <div>
      <h1 className="text-center">Owners</h1>
      <Divider></Divider>
      <div className="m-5">
        <Table
          aria-label="Owners table"
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
                onChange={setPage}
              />
            </div>
          }
        >
        <TableHeader columns={ownerColumnsTable}>
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
  );  
}

