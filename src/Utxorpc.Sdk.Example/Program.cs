﻿using Utxorpc.Sdk;
using Utxorpc.Sdk.Models;
using Utxorpc.Sdk.Models.Enums;

SyncServiceClient? syncServiceClient = new("http://localhost:50051");

await foreach (NextResponse? response in syncServiceClient.FollowTipAsync(
    new BlockRef
    (
        "1dace9bc646e9225251db04ff27397c199b04ec3f83c94cad28c438c3e7eeb50",
        67823979
    )))
{
    Console.WriteLine("___________________");
    switch (response.Action)
    {
        case NextResponseAction.Apply:
            Block? applyBlock = response.AppliedBlock;
            if (applyBlock is not null)
            {
                Console.WriteLine($"Apply Block: {applyBlock.Hash} Slot: {applyBlock.Slot}");
                Console.WriteLine($"Cbor: {Convert.ToHexString(applyBlock.NativeBytes ?? [])}");
            }
            break;
        case NextResponseAction.Undo:
            Block? undoBlock = response.UndoneBlock;
            if (undoBlock is not null)
            {
                Console.WriteLine($"Undo Block: {undoBlock.Hash} Slot: {undoBlock.Slot}");
            }
            break;
        case NextResponseAction.Reset:
            BlockRef? resetRef = response.ResetRef;
            if (resetRef is not null)
            {
                Console.WriteLine($"Reset to Block: {resetRef.Hash} Slot: {resetRef.Index}");
            }
            break;
    }
    Console.WriteLine("___________________");
}
