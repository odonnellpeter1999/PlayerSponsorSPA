import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/club/$teamslug/dashboard')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_forms/club/dashboard"!</div>
}
