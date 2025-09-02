/**
 * @jest-environment jsdom
 */
import '@testing-library/jest-dom'
import { render, screen } from '@testing-library/react'
import HomePage from '@/app/page'
 
describe('HomePage', () => {
  it('renders a heading', () => {
    render(<HomePage />);
 
    const heading = screen.getByRole('heading', { level: 1 });
    expect(heading).toBeInTheDocument();
  });

  it('renders view all buttons', () => {
    render(<HomePage />);

    const viewAllButtons = screen.getAllByRole('button', {name:'View All'});
    expect(viewAllButtons).toHaveLength(2);

    viewAllButtons.forEach((button) => {
      expect(button).toBeEnabled();
    });
  });
});