import { createFileRoute } from '@tanstack/react-router'
import SponsorshipPageSimplified from '../../components/SponsorshipPageSimplified'

export const Route = createFileRoute('/(storefront)/club')({
  component: RouteComponent,
})

function RouteComponent() {
  return <SponsorshipPageSimplified></SponsorshipPageSimplified>
}
