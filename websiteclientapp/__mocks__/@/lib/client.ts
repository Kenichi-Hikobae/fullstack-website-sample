
import { OwnerDTO, PropertyDTO } from '@/lib/api/apiClient';

const propertyMockData : PropertyDTO = {
  name: "name",
  address: "address",
  codeInternal: 1234566,
  id: "654654",
  ownerId: "987987",
  price: 22668844,
  propertyType: 0,
  propertyImages: [],
  propertyTraces: [],
  year: 2024,
};

const ownerMockData : OwnerDTO = {
  name: "name",
  address: "address",
  birthday: new Date(),
  email: "email@email.com",
  id: "4654654",
  photo: "photo"       
};

export const api = {
  property: {
    getProperty: jest.fn().mockResolvedValue(propertyMockData),
    getProperties: jest.fn().mockResolvedValue([
      propertyMockData
    ]),
    getTotalCount: jest.fn().mockResolvedValue(2),
  },
  owner: {
    getAllOwners: jest.fn().mockResolvedValue([
      ownerMockData,
    ]),
  },
};

// export class PropertyClient {
//   getProperty = jest.fn().mockResolvedValue({
//     propertyMockData
//   });
  
//   getProperties = jest.fn().mockResolvedValue([
//     propertyMockData,
//   ]);
  
//   getTotalCount = jest.fn().mockResolvedValue(2);
// }

// export class OwnerClient {
//   getAllOwners = jest.fn().mockResolvedValue([
//     ownerMockData,
//   ]);
// }