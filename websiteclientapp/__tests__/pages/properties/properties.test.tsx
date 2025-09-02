/**
 * @jest-environment jsdom
 */
import '@testing-library/jest-dom'
import { render, screen, waitFor } from '@testing-library/react'

jest.mock("@/lib/client");

import PropertiesPage from '@/app/properties/page'

describe('PropertiesPage', () => {
  it('renders a heading', () => {
    render(<PropertiesPage />);
    
    const headings = screen.getAllByRole("heading", { level: 1 });
    expect(headings[0]).toHaveTextContent("Properties");
  });
});