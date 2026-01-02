        ┌──────────────┐
        │   Program    │
        │ (Scheduler)  │
        └──────┬───────┘
               │ enqueue
        ┌──────▼───────┐
        │  LocalQueue  │   ← Channel<T>
        └──────┬───────┘
               │ dequeue
     ┌─────────▼──────────┐
     │ QueueProcessor     │
     │ (Parallel Workers) │
     └─────────┬──────────┘
               │
        ┌──────▼──────────┐
        │ PdfScannerSvc   │  ← Orchestrator
        └──────┬──────────┘
   ┌───────────┼────────────┐
   │           │            │
┌──▼──┐   ┌────▼────┐  ┌────▼─────┐
│ MCP │   │ RAG     │  │ LLM      │
│ PDF │   │ Indexer │  │ Scorer   │
└──┬──┘   └────┬────┘  └────┬─────┘
   │            │            │
   ▼            ▼            ▼
Text        Vector Store   Confidence
                                 │
                           ┌─────▼─────┐
                           │ AlertSvc  │
                           └───────────┘
