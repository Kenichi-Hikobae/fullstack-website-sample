/**
 * The client class that initializes the connection to the Client API.
 **/
import { OwnerClient, PropertyClient } from "./api/apiClient";

const baseUrl = process.env.NEXT_PUBLIC_API_URL || "https://localhost:5201";
export const api = {
  owner: new OwnerClient(baseUrl),
  property: new PropertyClient(baseUrl),
};
