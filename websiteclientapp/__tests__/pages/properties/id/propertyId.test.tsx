/**
 * @jest-environment jsdom
 */
import '@testing-library/jest-dom'
import { render, screen, waitFor } from '@testing-library/react'

jest.mock("@/lib/client");
jest.mock("next/navigation", () => ({
  useParams: jest.fn().mockReturnValue({ id: "123" }),
  useRouter: jest.fn().mockReturnValue({ push: jest.fn(), replace: jest.fn() }),
}));

import PropertyId from '@/app/properties/[id]/page'

describe('PropertyId', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('renders a heading', () => {
    render(<PropertyId />) 
    waitFor(() =>{
      const heading = screen.getByRole('heading', { level: 1 });
      expect(heading).toBeInTheDocument();
    }); 
  });
});