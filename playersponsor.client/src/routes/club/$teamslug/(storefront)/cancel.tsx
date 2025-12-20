import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/club/$teamslug/(storefront)/cancel')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/club/$teamslug/(storefront)/cancel"!</div>
}
