export default function About() {
  return (
    <main>
      <section className="bg-black text-white px-5 py-24">
        <div className="max-w-5xl mx-auto">
          <p className="uppercase tracking-[.3em] text-xs text-gray-400">Our story</p>
          <h1 className="font-display text-6xl mt-4">Simple joy.<br/>Real taste.</h1>
          <p className="text-gray-300 text-lg max-w-2xl mt-8 leading-8">
            NEELCOCO is built around a simple idea: milk should taste like a happy memory.
            We create accessible dairy treats with a focus on flavour, consistency and family moments.
          </p>
        </div>
      </section>
      <section className="max-w-5xl mx-auto px-5 py-20 grid md:grid-cols-3 gap-6">
        {["Quality", "Taste", "Trust"].map(x => <div className="border rounded-3xl p-8" key={x}><h2 className="font-display text-3xl">{x}</h2><p className="text-gray-500 mt-4">A core part of the NEELCOCO promise.</p></div>)}
      </section>
    </main>
  );
}
