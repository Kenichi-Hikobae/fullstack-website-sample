/**
 * @jest-environment jsdom
 */
import '@testing-library/jest-dom'
import { render, screen } from '@testing-library/react'

jest.mock("@/lib/client");

import OwnersPage, { ownerColumnsTable } from '@/app/owners/page'

describe('OwnersPage', () => {
  it('renders a heading', () => {
    render(<OwnersPage />);
 
    const heading = screen.getByRole('heading', { level: 1 }); 
    expect(heading).toBeInTheDocument();
  });

  it('renders a Table', () => {
    render(<OwnersPage />);

    const table = screen.getByRole("grid", { name: /owners table/i });
    expect(table).toBeInTheDocument();

    const headers = screen.getAllByRole("columnheader");
    expect(headers).toHaveLength(ownerColumnsTable.length);
    expect(headers.map(h => h.textContent)).toEqual(["ID", "NAME", "EMAIL", "ADDRESS", "PHOTO", "BIRTHDAY", "ACTIONS"]);
  });
});